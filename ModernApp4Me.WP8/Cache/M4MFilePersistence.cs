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
        
        /// <summary>
        /// Reads a file from the Isolated StorageFile.
        /// </summary>
        /// <param name="fileName">the name of the file to read</param>
        /// <returns>the content of the file in case of success, null otherwise</returns>
        public string ReadFile(string fileName)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Reading the file with the name : '" + fileName + "'");
                }

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
                            if (M4MModernLogger.Instance.IsWarnEnabled() == true)
                            {
                                M4MModernLogger.Instance.Warn("The file with the name : '" + fileName + "' cannot be found");
                            }

                            fileContent = null;
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsWarnEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while reading the file with the name : '" + fileName + "'", exception);
                    }
                }

                return fileContent;
            }
        }

        /// <summary>
        /// Writes a <see cref="string"/> into a file located into the Isolated Storage.
        /// </summary>
        /// <param name="fileName">the file name</param>
        /// <param name="fileContent">the file content</param>
        /// <param name="fileMode">the <see cref="FileMode"/></param>
        /// <returns>true in case of success. false otherwise</returns>
        public bool WriteFile(string fileName, string fileContent, FileMode fileMode)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Writing into the file with the name : '" + fileName + "'");
                }

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
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while writing into the file with the name : '" + fileName + "'", exception);
                    }

                    isWritten = false;
                }

                return isWritten;
            }
        }

        /// <summary>
        /// Deletes a file from the Isolated Storage.
        /// </summary>
        /// <param name="fileName">the name of the file to delete.</param>
        /// <returns>true in case of success. false otherwise</returns>
        public bool DeleteFile(string fileName)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Deleting the file with the name : '" + fileName + "'");
                }
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
                            if (M4MModernLogger.Instance.IsWarnEnabled() == true)
                            {
                                M4MModernLogger.Instance.Warn("The file with the name : '" + fileName + "' cannot be found");
                            }

                            isDeleted = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while deleting the file with the name : '" + fileName + "'", exception);
                    }

                    isDeleted = false;
                }

                return isDeleted;
            }
        }

        /// <summary>
        /// Cleans-up the Isolated Storage
        /// </summary>
        /// <returns>true in case of success. false otherwise</returns>
        public bool CleanupIsolatedStorage()
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Cleaning-up the isolated storage");
                }

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
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while cleaning-up the isolated storage", exception);
                    }

                    isCleaned = false;
                }

                return isCleaned;
            }
        }

        /// <summary>
        /// Checks if a file exists into the Isolated Storage
        /// </summary>
        /// <param name="fileName">the name of the file</param>
        /// <returns>true if the file exists, false otherwise</returns>
        public bool IsFileExists(string fileName)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Checking if the file '" + fileName + "' exists");
                }

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
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while checking if the file with the name : '" + fileName + "' exists", exception);
                    }

                    isFileExists = false;
                }

                return isFileExists;
            }
        }

        /// <summary>
        /// Creates a directory into the Isolated Storage.
        /// </summary>
        /// <param name="directoryName">the name of the directory</param>
        /// <returns>true in case of success. false otherwise</returns>
        public bool CreateDirectory(string directoryName)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Creating the directory with the name '" + directoryName + "'");
                }

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
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while creating the directory with the name : '" + directoryName + "' exists", exception);
                    }

                    isSucceed = false;
                }

                return isSucceed;
            }
        }

        /// <summary>
        /// Creates a directory from a complete path into the Isolated Storage
        /// </summary>
        /// <param name="filePath">the complete path</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool CreateDirectoryFromFilePath(string filePath)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Creating the directory from file path '" + filePath + "'");
                }

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
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while creating the directory from file path the name : '" + filePath + "' exists", exception);
                    }

                    isSucceed = false;
                }

                return isSucceed;
            }
        }

        /// <summary>
        /// Reads a binary contents from a file into the Isolated Storage
        /// </summary>
        /// <param name="fileName">the file to read</param>
        /// <returns>the content in case of success, null otherwise</returns>
        public MemoryStream ReadBinary(string fileName)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Reading the binary with name '" + fileName + "'");
                }

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
                            if (M4MModernLogger.Instance.IsWarnEnabled() == true)
                            {
                                M4MModernLogger.Instance.Warn("The file with the name : '" + fileName + "' cannot be found");
                            }

                            stream = null;
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while reading the the file with name : '" + fileName + "'", exception);
                    }

                    stream = null;
                }

                return stream;
            }
        }
    }
}