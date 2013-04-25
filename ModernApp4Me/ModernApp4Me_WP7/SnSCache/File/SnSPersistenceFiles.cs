using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Windows;
using ImageTools;
using ImageTools.IO.Png;
using System.Windows.Media.Imaging;
using ModernApp4Me_WP7.SnSLog;

namespace ModernApp4Me_WP7.SnSCache.File
{
    /// <summary>
    /// Provides functions to manipulate files, images, etc. from the isolated storage.
    /// Implementing the singleton pattern.
    /// Thread Safety because of the mutex.
    /// </summary>
    public sealed class SnSPersistenceFiles
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private static volatile SnSPersistenceFiles _instance;
        private static readonly object InstanceLock = new Object();
        private readonly Mutex _mutex;


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Private constructor.
        /// </summary>
        private SnSPersistenceFiles()
        {
            _mutex = new Mutex(true, "isolated storage file mutex");
        }

        /// <summary>
        /// Returns the current instance.
        /// </summary>
        /// <returns></returns>
        public SnSPersistenceFiles GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SnSPersistenceFiles();
                    }
                }
            }

            return _instance;
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
                _mutex.WaitOne();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
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
                    });
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "ReadFile");
                fileContent = null;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }

            return fileContent;
        }

        /// <summary>
        /// Function that write into a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        /// <param name="fileMode"></param>
        /// <returns>true in case of success</returns>
        public bool WriteFile(string fileName, string fileContent, FileMode fileMode)
        {
            var isSucceed = true;

            try
            {
                _mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //The file name includes the directory
                    if (fileName.Split('/').Length > 0)
                    {
                        CreateDirectoryFromFilePath(fileName);
                    }

                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var fileStream = new IsolatedStorageFileStream(fileName, fileMode, isf))
                        {
                            using (var streamWriter = new StreamWriter(fileStream))
                            {
                                streamWriter.WriteLine(fileContent);
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "WriteFile");
                isSucceed = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Deletes a file from the isolated storage.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>true in case of success</returns>
        public bool DeleteFile(string fileName)
        {
            var isSucceed = true;

            try
            {
                _mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (isf.FileExists(fileName))
                        {
                            isf.DeleteFile(fileName);
                        }
                    }
                });

                _mutex.ReleaseMutex();
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "DeleteFile");
                isSucceed = false;
            }
           finally
            {
                _mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Clears all the isolatedStorage.
        /// </summary>
        /// <returns>true in case of success</returns>
        public bool ClearIsolatedStorage()
        {
            var isSucceed = true;

            try
            {
                _mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isf.Remove();
                    }
                });
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "ClearIsolatedStorage");
                isSucceed = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool IsFileExists(string fileName)
        {
            var isFileExists = false;

            try
            {
                _mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isFileExists = isf.FileExists(fileName);
                    }
                });
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "IsFileExists");
                isFileExists = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
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
                _mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //The image name includes the directory
                    if (imageName.Split('/').Length > 0)
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
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "WriteImageAsPng");
                isSucceed = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
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
                _mutex.WaitOne();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //The image name includes the directory
                    if (imageName.Split('/').Length > 0)
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
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "WriteImageAsPng");
                isSucceed = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
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
                _mutex.WaitOne();
                
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isf.CreateDirectory(directoryName);
                    }
                });
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "CreateDirectory");
                isSucceed = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
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

                for (int i = 0; i < directoryPathTab.Length - 1; i++)
                {
                    directoryPath.Append(directoryPathTab[i]);
                    directoryPath.Append('/');
                }

                _mutex.WaitOne();
                
                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    isf.CreateDirectory(directoryPath.ToString());
                }
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "CreateDirectoryFromFilePath");
                isSucceed = false;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }

            return isSucceed;
        }

        /// <summary>
        /// Returns an image from the isolated storage.
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public BitmapImage ReadImage(string imageName)
        {
            BitmapImage image = null;

            try
            {
                _mutex.WaitOne();
                   
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (isf.FileExists(imageName))
                        {
                            using (var fileStream = isf.OpenFile(imageName, FileMode.Open, FileAccess.Read))
                            {
                                image = new BitmapImage();
                                image.SetSource(fileStream);
                            }
                        }
                        else
                        {
                            throw new FileNotFoundException(imageName);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceFiles", "ReadImage");
                image = null;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }

            return image;
        }
    }
}