using System;
using System.IO;
using System.IO.IsolatedStorage;
using wp4me.SnSDebugUtils;

namespace wp4me.SnSIsolatedStorageUtils
{
    /// <summary>
    /// Class that provides functions to write or read files from the IsolatedStorage.
    /// </summary>
    public sealed class SnSIsolatedStorageFile
    {
        /// <summary>
        /// Function that read the param file's content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>the file's content or null if an error occurred</returns>
        public static string ReadFile(string fileName)
        {
            string fileContent;

            try
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
            }
            catch (IsolatedStorageException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
            catch (ArgumentNullException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
            catch (ArgumentException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
            catch (OutOfMemoryException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
            catch (FileNotFoundException e)
            {
                SnSDebug.ConsoleWriteLine("File not found : " + e.FileName);
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
            catch (IOException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
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
        public static bool WriteFile(string fileName, string fileContent, FileMode fileMode)
        {
            try
            {
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
            }
            catch (IsolatedStorageException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ArgumentNullException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ArgumentException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ObjectDisposedException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (IOException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete a file from the isolated storage.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>true in case of success</returns>
        public static bool DeleteFile(string fileName)
        {
            try
            {
                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists(fileName))
                    {
                        isf.DeleteFile(fileName);
                    }
                }
            }
            catch (IsolatedStorageException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ArgumentNullException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ArgumentException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ObjectDisposedException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clear all the isolatedStorage.
        /// </summary>
        /// <returns>true in case of success</returns>
        public static bool ClearIsolatedStorage()
        {
            try
            {
                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    isf.Remove();
                }
            }
            catch (IsolatedStorageException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if a file exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileExists(string fileName)
        {
            try
            {
                using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isf.FileExists(fileName);
                }
            }
            catch (IsolatedStorageException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (ObjectDisposedException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return false;
            }
        }
    }
}