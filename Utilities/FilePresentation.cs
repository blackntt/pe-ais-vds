using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VDS_New.Utilities
{
    class FilePresentation
    {

        string[,] defaultArray;
        string trainingVirusFolderPath = "Virus_Training";
        string trainingBenignFolderPath = "Benign_Training";
        string testingVirusFolderPath = "Virus_Testing";
        string testingBenignFolderPath = "Benign_Testing";
        string trainingFolder;
        string formatFilePath = "input.txt";

        public FilePresentation() { }
        public FilePresentation(string trainingFolder)
        {
            this.trainingFolder = trainingFolder;
            //Tao cac d uong dan den noi chua du lieu training va test virus
            trainingVirusFolderPath = Path.Combine(trainingFolder, trainingVirusFolderPath);
            testingVirusFolderPath = Path.Combine(trainingFolder, testingVirusFolderPath);
            //Tao cac d uong dan den noi chua du lieu training va test benign file
            trainingBenignFolderPath = Path.Combine(trainingFolder, trainingBenignFolderPath);
            testingBenignFolderPath = Path.Combine(trainingFolder, testingBenignFolderPath);
        }

        public void PresentFiles()
        {

            //Dumpbin cac file
            ExcuteCommand(trainingVirusFolderPath, trainingVirusFolderPath + "_Export");
            ExcuteCommand(testingVirusFolderPath, testingVirusFolderPath + "_Export");
            ExcuteCommand(trainingBenignFolderPath, trainingBenignFolderPath + "_Export");
            ExcuteCommand(testingBenignFolderPath, testingBenignFolderPath + "_Export");

            //Vector hoa
            DumpbinFile2Vector(formatFilePath, trainingVirusFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Virus_Training.csv"));
            DumpbinFile2Vector(formatFilePath, testingVirusFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Virus_Testing.csv"));
            DumpbinFile2Vector(formatFilePath, trainingBenignFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Benign_Training.csv"));
            DumpbinFile2Vector(formatFilePath, testingBenignFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Benign_Testing.csv"));
            //Training Feature Normalization

            TrainingNormalize normalizor = new TrainingNormalize();
            normalizor.basic_feature = File.ReadAllLines(formatFilePath).Length;//52
            string benign_input_path = Path.Combine(trainingFolder, "Benign_Training.csv");
            string virus_input_path = Path.Combine(trainingFolder, "Virus_Training.csv");
            string benign_output_path = Path.Combine(trainingFolder, "Normal_Benign_Training.csv");
            string virus_output_path = Path.Combine(trainingFolder, "Normal_Virus_Training.csv");
            string minmax_output_path = Path.Combine(trainingFolder, "Min_Max.csv");
            normalizor.GetData(@benign_input_path, @virus_input_path);
            normalizor.MinMax();
            normalizor.ExportCSV(normalizor.benign_data, benign_output_path);
            normalizor.ExportCSV(normalizor.virus_data, virus_output_path);
            normalizor.SaveMinMax(@minmax_output_path, normalizor.listMin, normalizor.listMax);

        }
        public void PresentFiles(Action<int, object> callback)
        {
            int total_step = 10;
            float cur_step = 0.0f;
            //Dumpbin cac file
            ExcuteCommand(trainingVirusFolderPath, trainingVirusFolderPath + "_Export");
            callback((int)((++cur_step / total_step) * 100),null);
            ExcuteCommand(testingVirusFolderPath, testingVirusFolderPath + "_Export");
            callback((int)((++cur_step / total_step) * 100), null);
            ExcuteCommand(trainingBenignFolderPath, trainingBenignFolderPath + "_Export");
            callback((int)((++cur_step / total_step) * 100), null);
            ExcuteCommand(testingBenignFolderPath, testingBenignFolderPath + "_Export");
            callback((int)((++cur_step / total_step) * 100), null);
            //Vector hoa
            DumpbinFile2Vector(formatFilePath, trainingVirusFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Virus_Training.csv"));
            callback((int)((++cur_step / total_step) * 100), null);
            DumpbinFile2Vector(formatFilePath, testingVirusFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Virus_Testing.csv"));
            callback((int)((++cur_step / total_step) * 100), null);
            DumpbinFile2Vector(formatFilePath, trainingBenignFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Benign_Training.csv"));
            callback((int)((++cur_step / total_step) * 100), null);
            DumpbinFile2Vector(formatFilePath, testingBenignFolderPath + "_Export",
                                Path.Combine(trainingFolder, "Benign_Testing.csv"));
            callback((int)((++cur_step / total_step) * 100), null);
            //Training Feature Normalization

            TrainingNormalize normalizor = new TrainingNormalize();
            normalizor.basic_feature = File.ReadAllLines(formatFilePath).Length;//52
            string benign_input_path = Path.Combine(trainingFolder, "Benign_Training.csv");
            string virus_input_path = Path.Combine(trainingFolder, "Virus_Training.csv");
            string benign_output_path = Path.Combine(trainingFolder, "Normal_Benign_Training.csv");
            string virus_output_path = Path.Combine(trainingFolder, "Normal_Virus_Training.csv");
            string minmax_output_path = Path.Combine(trainingFolder, "Min_Max.csv");
            normalizor.GetData(@benign_input_path, @virus_input_path);
            normalizor.MinMax();
            normalizor.ExportCSV(normalizor.benign_data, benign_output_path);
            normalizor.ExportCSV(normalizor.virus_data, virus_output_path);
            normalizor.SaveMinMax(@minmax_output_path, normalizor.listMin, normalizor.listMax);
            callback((int)((++cur_step / total_step) * 100), null);
        }

        #region Dumpbin
        public void ExcuteCommand(string folder, string exportingPath)
        {
            FileInfo[] filesInFolder = (new DirectoryInfo(folder)).GetFiles();//.GetFiles("*.*");
            //string exportingPath = System.IO.Path.Combine(folder, "Exported");

            if (Directory.Exists(exportingPath))
            {
                Directory.Delete(exportingPath, true);
            }
            Directory.CreateDirectory(exportingPath);

            //string Path = @"E:\Visual 2017\Common7\Tools\VsDevCmd.bat";


            for (int i = 0; i < filesInFolder.Length; i++)
            {
                //string excutedCommand = "dumpbin /headers /imports "
                //                        + "\"" + filesInFolder[i].FullName + "\""
                //                        + " > " + "\"" + Exportlink(filesInFolder[i], exportingPath) + "\"";
                //ExecuteCommandSync(excutedCommand);

                ExecuteCommandAFile(filesInFolder[i].FullName, exportingPath);
            }
        }
        public void ExecuteCommandAFile(string filePath,string storeExportingPath)
        {

            FileInfo filesInFolder = new FileInfo(filePath);
            string command = "dumpbin /headers /imports "
                                        + "\"" + filesInFolder.FullName + "\""
                                        + " > " + "\"" + Exportlink(filesInFolder, storeExportingPath) + "\"";
            try
            {
                ProcessStartInfo procStartInfo =
                    new ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }
        public void ExecuteCommandSync(object command)
        {
            try
            {
                ProcessStartInfo procStartInfo =
                    new ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }
        public string Exportlink(FileInfo fileInfo, string pathtoStoredFolder)
        {
            string exportedFile = ChangeFileExtension(fileInfo.Name.ToString());
            string exportedPath = pathtoStoredFolder + "\\" + exportedFile;
            if (File.Exists(exportedPath))
                File.Delete(exportedPath);
            return exportedPath;
        }
        public string ChangeFileExtension(string name)
        {
            int position = name.IndexOf('.');
            name = name.Remove(position + 1);
            name = name + "txt";
            return name;
        }
        #endregion

        #region Vector hoa
        private void DumpbinFile2Vector(string formatFile, string sourceFolder, string destFile)
        {
            if (File.Exists(destFile))
                File.Delete(destFile);
            defaultArray = null;
            FileInfo[] files;
            int idOfCSV = 1;
            LoadInputText(formatFile);
            CreateCSV(destFile);
            files = (new DirectoryInfo(sourceFolder)).GetFiles("*.txt");
            foreach (FileInfo temppath in files)
            {
                ExtractFeatures(temppath.FullName);
                PushtoCSV(destFile, temppath.Name, idOfCSV);
                idOfCSV++;
            }
        }


        private void PushtoCSV(string filepath, string filename, int idOfCSV)
        {

            string line = Environment.NewLine + idOfCSV + "," + filename + ",";
            for (int i = 0; i < defaultArray.GetLength(0); i++)
            {
                line += defaultArray[i, 1];
                if (i < defaultArray.GetLength(0) - 1)
                {
                    line += ",";
                }
            }
            File.AppendAllText(filepath, line);

            CleanData();

        }
        private void CleanData()
        {
            for (int i = 0; i < defaultArray.GetLength(0); i++)
            {
                defaultArray[i, 1] = "";
            }
        }

        private void LoadInputText(string path)
        {
            //TODO: Load Input
            string[] inputField = File.ReadAllLines(path);

            //TODO: Lower key
            //TODO: Remove whitespace
            for (int i = 0; i < inputField.Length; i++)
            {
                inputField[i] = inputField[i].Trim();
            }

            //CreateArray
            CreateArray(inputField);
        }
        private void CreateArray(string[] inputField)
        {
            defaultArray = new string[inputField.Length, 2];

            //Name X ases
            for (int i = 0; i < inputField.Length; i++)
            {
                defaultArray[i, 0] = inputField[i];
            }
            for (int i = 0; i < inputField.Length; i++)
            {
                defaultArray[i, 1] = "";
            }
        }
        private void CreateCSV(string destPath)
        {
            string line = "id,name,";
            for (int i = 0; i < defaultArray.GetLength(0); i++)
            {
                line += defaultArray[i, 0];
                if (i < defaultArray.GetLength(0) - 1)
                {
                    line += ",";
                }

            }
            File.WriteAllText(destPath, line.ToString());
        }
        private void ExtractFeatures(string filePath)
        {
            string[] datatoTest =
               {
            ".reloc",
            ".rdata",
            ".rsrc",
            "characteristics"
        };
            string[] dataRVA =
            {
            "RVA [size] of Certificates Directory",
            "RVA [size] of Load Configuration Directory",
            "RVA [size] of Debug Directory",
            "RVA [size] of Export Directory",
            "RVA [size] of Base Relocation Directory",
            "RVA [size] of Bound Import Directory"
        };
            List<string> datatoTestList;
            List<string> dataRVAList;

            datatoTestList = new List<string>(datatoTest);
            dataRVAList = new List<string>(dataRVA);
            string defString;
            string line;
            string linetempheck;
            //bool is_stop = false;
            using (StreamReader file = new StreamReader(filePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    for (int i = 0; i < defaultArray.GetLength(0); i++)
                    {
                        defString = defaultArray[i, 0].ToString();
                        if (line.ToUpper().Contains(defString.ToUpper()))//doi qua Upper roi check
                        {
                            if (defString.Contains(".dll"))
                            {
                                linetempheck = file.ReadLine();
                                if (linetempheck.Contains("Import Address Table"))
                                {
                                    defaultArray[i, 1] = "1";
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            line = line.Replace(defaultArray[i, 0], "");
                            line = line.Replace(" ", "");
                            if (defString == "checksum")
                            {
                                if (line.Length > 10)
                                {
                                    break;
                                }
                                if (defaultArray[i, 1].Length > 0)
                                {
                                    break;
                                }

                            }
                            else if (datatoTestList.Contains(defString))
                            {
                                if (line.Length > 10)
                                {
                                    break;
                                }
                                if (!line.Any(char.IsDigit))
                                {
                                    break;
                                }
                                if (line.Contains("name"))
                                {
                                    break;
                                }
                            }
                            else if (dataRVA.Contains(defString))
                            {
                                line = RemoveBetween(line, '[', ']');
                            }
                            else if (defString == "image base")
                            {
                                line = RemoveBetween(line, '(', ')');
                            }
                            //string temp = defaultArray[i, 0].ToString();
                            if (defString != "image version"
                                && defString != "image base")
                            {
                                line = (int.Parse(line.ToString(), System.Globalization.NumberStyles.HexNumber)).ToString();
                            }
                            defaultArray[i, 1] = line;

                        }
                    }
                    if (line.Contains("RAW DATA"))
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < defaultArray.GetLength(0); i++)
            {
                if (defaultArray[i, 1].Equals(""))
                {
                    defaultArray[i, 1] = "0";//default "-1"
                }
            }
        }
        string RemoveBetween(string s, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }

        public double[] VectorizeAFile(string filePath)
        {
            LoadInputText(formatFilePath);
            FileInfo fileInfo = new FileInfo(filePath);
            ExtractFeatures(fileInfo.FullName);
            double[] featureVector = new double[defaultArray.GetLength(0)];
            for (int i = 0; i < featureVector.GetLength(0); i++)
            {
                float value = 0;
                try
                {
                    value = float.Parse(defaultArray[i, 1]);

                }
                catch (FormatException fe)
                {
                    int temp = Convert.ToInt32(defaultArray[i, 1], 16);
                    value = temp;
                }
                featureVector[i] = value;
            }
            return featureVector;
        }
        #endregion

      
        
        
    } 
    #region Normalize
    internal class TrainingNormalize
    {
        internal int numAttribute;
        internal int rowData;
        internal string[,] matrixData;
        internal string[,] benign_data;
        internal string[,] virus_data;
        internal int basic_feature;


        public void GetData(string benignpath, string viruspath)
        {
            //string[] temp = File.ReadAllLines(path);
            string[] benign_temp = File.ReadAllLines(benignpath);
            string[] virus_temp = File.ReadAllLines(viruspath);
            this.matrixData = new string[benign_temp.Length + virus_temp.Length - 1, benign_temp[0].Split(',').Length];
            this.benign_data = new string[benign_temp.Length, benign_temp[0].Split(',').Length];
            this.virus_data = new string[virus_temp.Length, virus_temp[0].Split(',').Length];
            for (int i = 0; i < benign_temp.Length; i++)
            {
                string[] values = benign_temp[i].Split(',');
                for (int j = 0; j < values.Length; j++)
                {
                    this.benign_data[i, j] = values[j];
                    this.matrixData[i, j] = values[j];
                }
            }
            for (int i = 0; i < virus_temp.Length; i++)
            {
                string[] values = virus_temp[i].Split(',');
                for (int j = 0; j < values.Length; j++)
                {
                    this.virus_data[i, j] = values[j];
                    if (i != 0)
                    {
                        this.matrixData[benign_temp.Length + i - 1, j] = values[j];
                    }
                }
            }


        }
        internal List<float> listMin;
        internal List<float> listMax;
        public void MinMax()
        {
            listMin = new List<float>();
            listMax = new List<float>();

            // Get min max of every attribute
            for (int i = 2; i < this.matrixData.GetLength(1); i++)
            {
                //float min = float.Parse(this.matrixData[1, i]);
                float min = getValueFromCell(this.matrixData, 1, i);
                float max = min;

                for (int j = 2; j < this.matrixData.GetLength(0); j++)
                {
                    // float value = float.Parse(this.matrixData[j, i]);
                    float value = getValueFromCell(this.matrixData, j, i);
                    if (value < min)
                    {
                        min = value;
                    }

                    if (value > max)
                    {
                        max = value;
                    }
                }

                listMin.Add(min);
                listMax.Add(max);
            }

            // Calculate new matrix
            for (int col = 2; col < this.benign_data.GetLength(1); col++)
            {
                int index = col - 2;
                for (int row = 1; row < this.benign_data.GetLength(0); row++)
                {

                    //float value = float.Parse(this.matrixData[row, col]);
                    float value = getValueFromCell(benign_data, row, col);
                    float result = value;
                    if (index < basic_feature)//
                    {
                        if (listMax[index] == listMin[index])
                            result = 0.5f;
                        else
                            result = (value - listMin[index]) /
                                       (listMax[index] - listMin[index]);
                        this.benign_data[row, col] = result.ToString();
                    }
                }
            }
            for (int col = 2; col < this.virus_data.GetLength(1); col++)
            {
                int index = col - 2;
                for (int row = 1; row < this.virus_data.GetLength(0); row++)
                {

                    //float value = float.Parse(this.matrixData[row, col]);
                    float value = getValueFromCell(virus_data, row, col);
                    float result = value;
                    if (index < basic_feature)//
                    {
                        if (listMax[index] == listMin[index])
                            result = 0.5f;
                        else
                            result = (value - listMin[index]) /
                                       (listMax[index] - listMin[index]);
                        this.virus_data[row, col] = result.ToString();
                    }
                }
            }
        }

        private float getValueFromCell(string[,] my_data, int row, int col)
        {
            float value = 0;
            try
            {
                value = float.Parse(my_data[row, col]);

            }
            catch (FormatException fe)
            {
                int temp = Convert.ToInt32(my_data[row, col], 16);
                value = temp;
            }
            return value;
        }
        public void ExportCSV(string[,] data, string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter writer = new StreamWriter(path);

            for (int row = 0; row < data.GetLength(0); row++)
            {
                string line = data[row, 0];

                for (int col = 1; col < data.GetLength(1); col++)
                {
                    line += "," + data[row, col];
                }

                writer.WriteLine(line);
            }

            writer.Close();
        }
        public void SaveMinMax(string path, List<float> listMin, List<float> listMax)
        {
            if (File.Exists(path))
                File.Delete(path);
            StreamWriter writer = new StreamWriter(@path);
            string line_min = "";
            string line_max = "";
            for (int i = 0; i < listMin.Count; i++)
            {
                if (i < listMin.Count - 1)
                {
                    line_min += listMin[i].ToString() + ',';
                    line_max += listMax[i].ToString() + ',';
                }
                else
                {
                    line_min += listMin[i].ToString();
                    line_max += listMax[i].ToString();
                }
            }
            writer.WriteLine(line_min);
            writer.WriteLine(line_max);
            writer.Close();

        }

    }
    #endregion
}
