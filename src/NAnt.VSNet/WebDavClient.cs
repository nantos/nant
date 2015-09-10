// NAnt - A .NET build tool
// Copyright (C) 2001-2003 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Matthew Mastracci (matt@aclaro.com)

using System;
using System.IO;
using System.Net;

namespace NAnt.VSNet {
    /// <summary>
    /// Implementation of a WebDAV client.
    /// </summary>
    public class WebDavClient {
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDavClient"/> class.
        /// </summary>
        /// <param name="uriBase">The URI base.</param>
        public WebDavClient(Uri uriBase) {
            _webProjectBaseUrl = uriBase.ToString();
        }

        #endregion Public Instance Constructors

        #region Public Instance Methods

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="localFileName">Name of the local file.</param>
        /// <param name="remoteFileName">Name of the remote file.</param>
        public void UploadFile(string localFileName, string remoteFileName) {
            WebRequest request = WebRequest.Create(_webProjectBaseUrl + "/" + remoteFileName);
            request.Method = "PUT";
            request.Headers.Add("Translate: f");
            request.Credentials = CredentialCache.DefaultCredentials;

            FileInfo fi = new FileInfo(localFileName);
            request.ContentLength = fi.Length;

            int bufferSize = 100 * 1024;
            byte[] buffer = new byte[bufferSize];
            using (FileStream fsInput = new FileStream(fi.FullName, FileMode.Open)) {
                using (Stream s = request.GetRequestStream()) {
                    int nRead;
                    do {
                        nRead = fsInput.Read(buffer, 0, bufferSize);
                        s.Write(buffer, 0, nRead);
                    }
                    while (nRead > 0);
                }
            }

            buffer = null;
            try {
                using (request.GetResponse()) {
                }
            } catch (WebException we) {
                HttpWebResponse hwr = ( HttpWebResponse )we.Response;
                
                if ((int) hwr.StatusCode != 423) {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="localFileName">Name of the local file.</param>
        /// <param name="remoteFileName">Name of the remote file.</param>
        public void DeleteFile(string localFileName, string remoteFileName) {
            WebRequest request = WebRequest.Create(_webProjectBaseUrl + "/" + remoteFileName);
            request.Method = "DELETE";
            request.Headers.Add("Translate: f");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (request.GetResponse()) {
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="localFileName">Name of the local file.</param>
        /// <param name="remoteFileName">Name of the remote file.</param>
        public void DownloadFile(string localFileName, string remoteFileName) {
            WebRequest request = WebRequest.Create(_webProjectBaseUrl + "/" + remoteFileName);
            request.Method = "GET";
            request.Headers.Add("Translate: f");
            request.Credentials = CredentialCache.DefaultCredentials;
            FileInfo fi = new FileInfo(localFileName);
            if (!Directory.Exists(fi.DirectoryName)) {
                Directory.CreateDirectory(fi.DirectoryName);
            }

            int bufferSize = 100 * 1024;
            byte[] buffer = new byte[bufferSize];
            using (FileStream fsOutput = new FileStream(fi.FullName, FileMode.OpenOrCreate)) {
                using (Stream s = request.GetResponse().GetResponseStream()) {
                    int nRead;
                    do {
                        nRead = s.Read(buffer, 0, bufferSize);
                        fsOutput.Write(buffer, 0, nRead);
                    } while (nRead > 0);
                }
            }

            buffer = null;
        }

        /// <summary>
        /// Gets the file contents.
        /// </summary>
        /// <param name="remoteFileName">Name of the remote file.</param>
        /// <returns>The file contents.</returns>
        public string GetFileContents(string remoteFileName) {
            WebRequest request = WebRequest.Create(_webProjectBaseUrl + "/" + remoteFileName);
            request.Method = "GET";
            request.Headers.Add("Translate: f");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream())) {
                return sr.ReadToEnd();
            }
        }

        #endregion Public Instance Methods

        #region Public Static Methods

        /// <summary>
        /// Gets the file contents.
        /// </summary>
        /// <param name="remoteFileName">Name of the remote file.</param>
        /// <returns>The file contents.</returns>
        public static string GetFileContentsStatic(string remoteFileName) {
            WebRequest request = WebRequest.Create(remoteFileName);
            request.Method = "GET";
            request.Headers.Add("Translate: f");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream())) {
                return sr.ReadToEnd();
            }
        }

        #endregion Public Static Methods

        #region Private Instance Fields

        private string _webProjectBaseUrl;

        #endregion Private Instance Fields
    }
}
