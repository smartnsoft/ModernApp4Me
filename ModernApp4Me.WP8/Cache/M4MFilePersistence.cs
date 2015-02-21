using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using ModernApp4Me.Core.Log;

namespace ModernApp4Me.WP8.Cache
{

    /// <summary>
    /// Enables to store some contents into the <see cref="IsolatedStorageFile"/>.
    /// The classe implements the singleton pattern and is thread safe !
    /// </summary>
    ///
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    // TODO : reworks totally this class.
    public sealed class M4MFilePersistence
    {
        private static volatile M4MFilePersistence instance;

        private static readonly object InstanceLock = new Object();

        private readonly Mutex mutex;

        private M4MFilePersistence()
        {
            mutex = new Mutex(false, "isolated storage file mutex");
        }

        public static M4MFilePersistence Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MFilePersistence();
                        }
                    }
                }

                return instance;
            }
        }
        
        public string ReadFile(string fileName)
        {
            string fileContent = null;

            try
            {
                mutex.WaitOne();
               
                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists(fileName))
                    {
                        using (var fileStream = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                        {
                            using (var streamReader = new StreamReader(fileStream))
                            {
                                fileContent = streamReader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException(fileName);
                    }
                }
            }
            catch (Exception exception)
            {
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot read the file '"+fileName+"'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return fileContent;
        }

        public bool WriteFile(string fileName, string fileContent, FileMode fileMode)
        {
            var isWritten = true;

            try
            {
                mutex.WaitOne();
               
                //The file name includes the directory
                if (fileName.Split('/').Length > 1)
                {
                    CreateDirectoryFromFilePath(fileName);
                }

                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var fileStream = new IsolatedStorageFileStream(fileName, fileMode, isf))
                    {
                        using (var streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.Write(fileContent);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                isWritten = false;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot write into the file '" + fileName + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isWritten;
        }

        public bool DeleteFile(string fileName)
        {
            var isDeleted = true;

            try
            {
                mutex.WaitOne();

                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists(fileName))
                    {
                        isf.DeleteFile(fileName);
                    }
                }
            }
            catch (Exception exception)
            {
                isDeleted = false;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot delete the file '" + fileName + "'", exception);
            }
           finally
            {
                mutex.ReleaseMutex();
            }

            return isDeleted;
        }

        public bool ClearIsolatedStorage()
        {
            var isCleaned = true;

            try
            {
                mutex.WaitOne();

                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    isf.Remove();
                }
            }
            catch (Exception exception)
            {
                isCleaned = false;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot clean-up the isolated storage", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isCleaned;
        }

        public bool IsFileExists(string fileName)
        {
            var isFileExists = true;

            try
            {
                mutex.WaitOne();

                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    isFileExists = isf.FileExists(fileName);
                }
            }
            catch (Exception exception)
            {
                isFileExists = false;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot check if the file '" + fileName + "' exists", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isFileExists;
        }

        public bool CreateDirectory(string directoryName)
        {
            var isSucceed = true;

            try
            {
                mutex.WaitOne();
                
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isf.CreateDirectory(directoryName);
                    }
                });
            }
            catch (Exception exception)
            {
                isSucceed = false;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot create the directory '" + directoryName + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        public bool CreateDirectoryFromFilePath(string filePath)
        {
            var isSucceed = true;

            try
            {
                //Building the directory path
                var directoryPathTab = filePath.Split('/');
                var directoryPath = new StringBuilder("/");

                for (var i = 0; i < directoryPathTab.Length - 1; i++)
                {
                    directoryPath.Append(directoryPathTab[i]);
                    directoryPath.Append('/');
                }

                mutex.WaitOne();
                
                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    isf.CreateDirectory(directoryPath.ToString());
                }
            }
            catch (Exception exception)
            {
                isSucceed = false;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot create the directory with path '" + filePath + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        public MemoryStream ReadBinary(string fileName)
        {
            MemoryStream stream;

            try
            {
                mutex.WaitOne();

                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists(fileName))
                    {
                        using (var fileStream = isf.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                        {
                            stream = new MemoryStream();
                            fileStream.CopyTo(stream);
                            fileStream.Close();
                            stream.Position = 0;
                        }
                    }
                    else
                    {
                        throw new Exception(fileName);
                    }
                }
            }
            catch (Exception exception)
            {
                stream = null;
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot read the binary file '" + fileName + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return stream;
        }
    }
}