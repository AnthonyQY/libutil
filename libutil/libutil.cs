using System;
using System.IO;
using System.Linq;
using System.Text;

namespace libutil
{
    public class UtilityEngine
    {
        string[] ReadFileAsBase64(string SourceFilePath)
        {
            string[] encodedFileData = null;

            try
            {
                string[] fileData = File.ReadAllLines(SourceFilePath);

                foreach (string data in fileData)
                {
                    encodedFileData.Append(Convert.ToBase64String(Encoding.ASCII.GetBytes(data)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return encodedFileData;
        }

        string[] ReadBase64FileAsASCII(string SourceFilePath)
        {
            string[] decodedFileData = null;

            try
            {
                string[] encodedFileData = File.ReadAllLines(SourceFilePath);

                foreach (string encodedData in encodedFileData)
                {
                    decodedFileData.Append(Encoding.ASCII.GetString(Convert.FromBase64String(encodedData)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return decodedFileData;
        }

        bool WriteFileAsBase64(string[] SourceData, string TargetFileDirectory, string TargetFileName, string TargetFileExtension, bool OverrideExisting)
        {
            string TargetFilePath = Path.Combine(TargetFileDirectory, TargetFileName + "." + TargetFileExtension);
            try
            {
                if (!File.Exists(TargetFilePath))
                {
                    using (var streamWriter = new StreamWriter(TargetFilePath, true))
                    {
                        foreach (string data in SourceData)
                        {
                            streamWriter.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(data)));
                        }
                    }
                    return true;
                }
                else
                {
                    if (OverrideExisting)
                    {
                        File.Delete(TargetFilePath);
                        using (var streamWriter = new StreamWriter(TargetFilePath, true))
                        {
                            foreach (string data in SourceData)
                            {
                                streamWriter.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(data)));
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
