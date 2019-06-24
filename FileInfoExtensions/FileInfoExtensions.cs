using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileInfoExtensions {
    public static class FileInfoExtensions {

        public static FileState GetFileState(this FileInfo fileInfo) {
            FileStream stream = null;

            try {
                stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            } catch(Exception ex) {
                if(ex is FileNotFoundException) {
                    return FileState.NotExistent;
                } else if(ex is IOException) {
                    return FileState.Locked;
                }

                return FileState.Unknown;
            } finally {
                if(stream != null) {
                    stream.Close();
                }
            }

            return FileState.Existent;
        }

        public static bool IsLocked(this FileInfo fileInfo) {
            FileStream stream = null;

            try {
                stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            } catch(IOException ex) {
                //the file is unavailable because it is
                //1. still being written to
                //2. or being processed by another thread
                //3. or does not exist (has already been processed)
                return true;
            } finally {
                if(stream != null) {
                    stream.Close();
                }
            }

            //file is not locked
            return false;
        }
    }
}
