/// ==============================================================
/// © VRGEN ALL RIGHTS RESERVED
/// ==============================================================
using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
namespace Utilities
{
    /// <summary>
    /// Class in charge of handling saving, loading and encryption of data
    /// </summary>
    public class FileUtility
    {
        public static string FILE_FOLDER = "/Save";              // Path for Saving
        private static string FILE_TYPE = "SAVE";

        /// ==============
        /// LOAD
        /// <summary>
        /// Loads SaveFile from Persistent Data path
        /// </summary>
        /// ==============
        public static object Load(string fileName, Type dataType)
        {
            string newLoad = LoadFile(Application.persistentDataPath + FILE_FOLDER, fileName, FILE_TYPE);
            return Deserialize(newLoad, dataType);
        }

        /// ==============
        /// LOAD
        /// <summary>
        /// Loads saved file from assigned directory
        /// </summary>
        /// ==============
        public static object Load(string directory, string fileName, Type dataType)
        {
            string newLoad = LoadFile(directory, fileName, FILE_TYPE);
            return Deserialize(newLoad, dataType);
        }

        /// <summary>
        /// Deserializes data stream with assigned data type
        /// </summary>
        /// <param name="newLoad"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private static object Deserialize(string newLoad, Type dataType)
        {
            // check if file DOES NOT exist
            if (newLoad == null || !File.Exists(newLoad))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(dataType);
            FileStream stream = new FileStream(newLoad, FileMode.Open);
            // make sure that file is deleted
            return serializer.Deserialize(stream);
        }
        /// ============
        /// LOAD FILE
        /// <summary>
        /// Creates temp XML file from decripted data and returns its path to be read for loading!
        /// </summary>
        /// <param name="directory">Directory where path is placed</param>
        /// <param name="filename">Name of temp path</param>
        /// <param name="filetype">Type of temp path</param>
        /// ===============
        private static string LoadFile(string directory, string filename, string filetype)
        {
            string path = directory + "/" + filename + "." + filetype;
            // check if file does not exist 
            if (!File.Exists(path))
            {
                Debug.LogWarning("FileUtility [WARNING] : " + path + " was not found!");
                return null;
            }
            try
            {
                string filedata = File.ReadAllText(path);
                //filedata = DecryptData(filedata);
                string tempFile = path;
                // create temporary file
                File.WriteAllText(tempFile, filedata);
                
                return tempFile;
            }
            catch (System.Exception e)
            {

                throw new System.Exception("Failed loading file", e);
            }
        }

        /// ==============
        /// SAVE
        /// <summary>
        /// Saves Game File at Persistent data path
        /// </summary>
        /// ==============
        public static void Save(string fileName, Logic.FileData data, Type dataType)
        {
            Serialize(Application.persistentDataPath, fileName, data, dataType);
        }

        /// ==============
        /// SAVE
        /// <summary>
        /// Saves file at given directory
        /// </summary>
        /// ==============
        public static void Save(string directory, string fileName, Logic.FileData data, Type dataType)
        {
            Serialize(directory, fileName, data, dataType);
        }

        /// ==============
        /// SERIALIZE
        /// <summary>
        /// Serializes data with provided properties
        /// </summary>
        /// ==============
        private static void Serialize(string path, string fileName, Logic.FileData data, Type dataType)
        {
            XmlSerializer serializerObj = new XmlSerializer(dataType);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            MemoryStream textWriter = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(textWriter, Encoding.UTF8);
            // serialize
            serializerObj.Serialize(xmlWriter, data);
            string result = Encoding.UTF8.GetString(textWriter.ToArray());
            CreateSaveFile(path, fileName, FILE_TYPE, result);
            textWriter.Close();
            xmlWriter.Close();
        }

        /// ==============
        /// CREATE SAVE FILE
        /// <summary>
        /// Creates and encrypts given file into a new file of given type.
        /// </summary>
        /// <param name="path">Path where it will be saved</param>
        /// <param name="filename">Name of the file</param>
        /// <param name="filetype">Format of the file</param>
        /// <param name="fileData">Info to save in the file</param>
        /// =============
        private static void CreateSaveFile(string path, string filename, string filetype, string fileData)
        {
            //string path = Application.persistentDataPath;
            // If directory does not exist in path, create one!
            if (!Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            // encrypt data
            //fileData = EncryptData(fileData);
            // write to path
            File.WriteAllText(path + "/" + filename + "." + filetype, fileData);
        }

        /// ================
        /// ENCRYPT DATA
        /// <summary>
        /// Encrypts assigned passed information into file
        /// </summary>
        /// <param name="toEncrypt">File to encrypt</param>
        /// <returns></returns>
        /// ================
        private static string EncryptData(string toEncrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");

            // 256-AES key
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();

            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;

            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// ================
        /// DECRYPT DATA
        /// <summary>
        /// Encrypts assigned passed information into file
        /// </summary>
        /// <param name="toDecrypt">File to decrypt</param>
        /// <returns></returns>
        /// ================
        private static string DecryptData(string toDecrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");

            // AES-256 key
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;

            rDel.Padding = PaddingMode.PKCS7;

            // better lang support
            ICryptoTransform cTransform = rDel.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
