using ModernApp4Me.WP8.Log;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

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
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Reading the file with the name : '" + fileName + "'");

                string fileContent = null;

                try
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (isf.FileExists(fileName) == true)
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
                            M4MModernLogger.Instance.Error("The file with the name : '" + fileName + "' cannot be found");
                            throw new FileNotFoundException(fileName);
                        }
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while reading the file with the name : '" + fileName + "'", exception);
                }

                return fileContent;
            }
        }

        public bool WriteFile(string fileName, string fileContent, FileMode fileMode)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Writing into the file with the name : '" + fileName + "'");

                var isWritten = true;

                try
                {
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
                    M4MModernLogger.Instance.Error("An error occurs while writing into the file with the name : '" + fileName + "'", exception);
                    isWritten = false;
                }

                return isWritten;
            }
        }

        public bool DeleteFile(string fileName)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Deleting the file with the name : '" + fileName + "'");
                var isDeleted = true;

                try
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (isf.FileExists(fileName) == true)
                        {
                            isf.DeleteFile(fileName);
                        }
                        else
                        {
                            throw new FileNotFoundException(fileName);
                        }
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while deleting the file with the name : '" + fileName + "'", exception);
                    isDeleted = false;
                }

                return isDeleted;
            }
        }

        public bool CleanupIsolatedStorage()
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Cleaning-up the isolated storage");

                var isCleaned = true;

                try
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isf.Remove();
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while cleaning-up the isolated storage", exception);
                    isCleaned = false;
                }

                return isCleaned;
            }
        }

        public bool IsFileExists(string fileName)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Checking if the file '" + fileName + "' exists");

                var isFileExists = true;

                try
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isFileExists = isf.FileExists(fileName);
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while checking if the file with the name : '" + fileName + "' exists", exception);
                    isFileExists = false;
                }

                return isFileExists;
            }
        }

        public bool CreateDirectory(string directoryName)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Creating the directory with the name '" + directoryName + "'");

                var isSucceed = true;

                try
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isf.CreateDirectory(directoryName);
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while creating the directory with the name : '" + directoryName + "' exists", exception);
                    isSucceed = false;
                }

                return isSucceed;
            }
        }

        public bool CreateDirectoryFromFilePath(string filePath)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Creating the directory from file path '" + filePath + "'");

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

                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        isf.CreateDirectory(directoryPath.ToString());
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while creating the directory from file path the name : '" + filePath + "' exists", exception);
                    isSucceed = false;
                }

                return isSucceed;
            }
        }

        public MemoryStream ReadBinary(string fileName)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Reading the binary with name '" + fileName + "'");

                MemoryStream stream;

                try
                {
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (isf.FileExists(fileName) == true)
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
                            throw new FileNotFoundException(fileName);
                        }
                    }
                }
                catch (Exception exception)
                {
                    M4MModernLogger.Instance.Error("An error occurs while reading the the file with name : '" + fileName + "'", exception);
                    stream = null;
                }

                return stream;
            }
        }
    }
}