using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using ImageTools;
using ImageTools.IO.Png;
using ModernApp4Me.WP7.SnSLog;

namespace ModernApp4Me.WP7.SnSCache.File
{

    /// <summary>
    /// Provides functions to manipulate files, images, etc. from the isolated storage.
    /// Implementing the singleton pattern.
    /// Thread Safety because of the mutex.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSFile
    {
        private static volatile SnSFile instance;

        private static readonly object InstanceLock = new Object();

        private readonly Mutex mutex;


        /// <summary>
        /// Private constructor.
        /// </summary>
        private SnSFile()
        {
            mutex = new Mutex(false, "isolated storage file mutex");
        }

        public static SnSFile Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new SnSFile();
                        }
                    }
                }

                return instance;
            }
        }
        
        /// <summary>
        /// Reads the param file's content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>the file's content or null if an error occurred</returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot read the file '"+fileName+"'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return fileContent;
        }

        /// <summary>
        /// Function that writes into a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        /// <param name="fileMode"></param>
        /// <returns>true in case of success</returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot write into the file '" + fileName + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isWritten;
        }

        /// <summary>
        /// Deletes a file from the isolated storage.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>true in case of success</returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot delete the file '" + fileName + "'", exception);
            }
           finally
            {
                mutex.ReleaseMutex();
            }

            return isDeleted;
        }

        /// <summary>
        /// Clears all the isolatedStorage.
        /// </summary>
        /// <returns>true in case of success</returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot clean-up the isolated storage", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isCleaned;
        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot check if the file '" + fileName + "' exists", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isFileExists;
        }

        /// <summary>
        /// Saves a PNG Image into the isolated storage.
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="image"></param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        public bool WriteImageAsPng(string imageName, BitmapImage image, FileMode fileMode)
        {
            var isSucceed = true;

            try
            {
                mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //The image name includes the directory
                    if (imageName.Split('/').Length > 1)
                    {
                        CreateDirectoryFromFilePath(imageName);
                    }

                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var fileStream = new IsolatedStorageFileStream(imageName, fileMode, isf))
                        {
                            using (var streamWriter = new StreamWriter(fileStream))
                            {
                                var encoder = new PngEncoder();
                                var writeableBitmap = new WriteableBitmap(image);
                                encoder.Encode(writeableBitmap.ToImage(), streamWriter.BaseStream);
                            }
                        }
                    }
                });
            }
            catch (Exception exception)
            {
                isSucceed = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot write the image '" + imageName + "' as PNG image", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Saves a PNG Image into the isolated storage.
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="image"></param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        public bool WriteImageAsPng(string imageName, ExtendedImage image, FileMode fileMode)
        {
            var isSucceed = true;

            try
            {
                mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //The image name includes the directory
                    if (imageName.Split('/').Length > 1)
                    {
                        CreateDirectoryFromFilePath(imageName);
                    }

                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (
                            var fileStream = new IsolatedStorageFileStream(imageName, fileMode, FileAccess.Write,
                                                                            isf))
                        {
                            using (var streamWriter = new StreamWriter(fileStream))
                            {
                                var encoder = new PngEncoder();
                                var writeableBitmap = new WriteableBitmap(image.ToBitmap());
                                encoder.Encode(writeableBitmap.ToImage(), streamWriter.BaseStream);
                            }
                        }
                    }
                });
            }
            catch (Exception exception)
            {
                isSucceed = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot write the image '" + imageName + "' as PNG image", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns>true in case of success</returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot create the directory '" + directoryName + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Creates a directory from the complete file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>true in case of success</returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot create the directory with path '" + filePath + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Returns a memory stream from the isolated storage.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
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
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot read the binary file '" + fileName + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return stream;
        }
    }
}