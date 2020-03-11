using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Utilities
{
    public class Globals
    {
        static Globals _globals = null;
        private double self_raidus;
        private double detector_radius;
        private double aff_scale;
        private double choosen_coeff;
        private double clonal_coeff;
        private double[] weights;
        private int virus_code;
        private int benign_code;
        private int basic_features;
        private double dlls_mutation;
        private double[] strategy_paras;
        private string machine_path;
        private string detector_path;
        private string input_txt_path;
        private string min_max_learning_path;
        public static double SELF_RADIUS { get => _globals.self_raidus; }
        public static double DETECTOR_RADIUS { get => _globals.detector_radius; }
        public static double AFF_SCALE { get => _globals.aff_scale; }
        public static double CHOOSEN_COEFF { get => _globals.choosen_coeff; }
        public static double CLONAL_COEFF { get => _globals.clonal_coeff; }
        public static double[] WEIGHTS { get => _globals.weights; }
        public static int VIRUS_CODE { get => _globals.virus_code; }
        public static int BENIGN_CODE { get => _globals.benign_code; }
        public static int BASIC_FEATURES { get => _globals.basic_features; }
        public static double DLLS_MUATION { get => _globals.dlls_mutation; }
        public static double[] STRATEGY_PARAS { get => _globals.strategy_paras; }
        public static string MACHINE_PATH { get => _globals.machine_path; set => _globals.machine_path=value; }
        public static string DETECTOR_PATH { get => _globals.detector_path; set => _globals.detector_path = value; }
        public static string INPUT_TXT_PATH { get => _globals.input_txt_path; set => _globals.input_txt_path = value; }
        public static string MIN_MAX_LEARNING_PATH { get => _globals.min_max_learning_path; set => _globals.min_max_learning_path=value; }
        private Globals()
        {
            
        }
        public static void LoadConfig()
        {
            if(_globals==null)
                _globals = new Globals();
            _globals.self_raidus = Properties.Settings.Default.SELF_RADIUS;
            _globals.detector_radius = Properties.Settings.Default.DETECTOR_RADIUS;
            _globals.aff_scale = Properties.Settings.Default.AFF_SCALE;
            _globals.choosen_coeff = Properties.Settings.Default.CHOOSEN_COEFF;
            _globals.clonal_coeff = Properties.Settings.Default.CLONAL_COEFF;
            string[] values = Properties.Settings.Default.WEIGHTS.Split(',');
            _globals.weights = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                _globals.weights[i] = double.Parse(values[i]);
            }
            _globals.virus_code = Properties.Settings.Default.VIRUS_CODE;
            _globals.benign_code = Properties.Settings.Default.BENIGN_CODE;
            _globals.basic_features = Properties.Settings.Default.BASIC_FEATURES;
            _globals.dlls_mutation = Properties.Settings.Default.DLLS_MUTATION;
            string[] stra_paras_values = Properties.Settings.Default.STRATEGY_PARAS.Split(',');
            _globals.strategy_paras = new double[stra_paras_values.Length];
            //for (int i = 0; i < values.Length; i++)
            //{
            //    _globals.weights[i] = double.Parse(values[i]);
            //}
            for (int i = 0; i < stra_paras_values.Length; i++)
            {
                _globals.strategy_paras[i] = double.Parse(stra_paras_values[i]);
            }
            _globals.machine_path = "";
            _globals.detector_path = "";
            _globals.input_txt_path = Properties.Settings.Default.INPUT_TXT_PATH;
            _globals.min_max_learning_path = "";
        }
        
    }
}
