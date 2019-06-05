using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileExtensions {
    public static class FileInfoExtensions {

        public static bool IsLocked(this FileInfo fileInfo) {
            FileStream stream = null;

            try {
                stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            } catch(IOException) {
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
