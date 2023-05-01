using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;

public class VBulkCarrier : MonoBehaviour
{
    private string[] data_tes;
   

    public string[] CallData
    {
        get
        {
            return data_tes;
        }
    }
    float draftCall;
    float TrimCall;
    float ListCall;
    public string editList = "2.5";
    public string editTrim = "2.5";

    public bool ModeMaterial = false;



    private UnitySerialPort data;//variabel untuk serial port
		//------------isi cargo
		public GameObject[] cargoList;
		//-----------------------------
		public GameObject[] sideTank;
		public GameObject[] doubleBottom;
		//public Transform lblCargoTank;
		//public Transform lblSideTank;
		private string message;
		private float vDraft;
		private float TrimVal;
		private float ListVal;
		public float kgData; // KG Data

		//chart transversal
        [SerializeField]
		public List<Vector2> pointSeries0;
		public List<Vector2> pointSeries1;
		public List<Vector2> pointSeries2;
		public List<Vector2> pointSeries3;
		public List<Vector2> pointSeries4;
		public List<Vector2> pointSeries5;
		public List<Vector2> pointSeries6;
		public List<Vector2> pointSeries7;
		public List<Vector2> pointSeries8;
		public List<Vector2> pointSeries9;
		public List<Vector2> pointSeries10;
		public List<Vector2> pointSeries11;
        public List<Vector2> pointSeries26;
        public List<Vector2> pointSeries27;
		//------------

		//chart Longitudinal
		public List<Vector2> pointSeries12;
		public List<Vector2> pointSeries13;
		public List<Vector2> pointSeries14;
		public List<Vector2> pointSeries15;
		public List<Vector2> pointSeries16;
		public List<Vector2> pointSeries17;
		public List<Vector2> pointSeries18;
		public List<Vector2> pointSeries19;
		public List<Vector2> pointSeries20;
		public List<Vector2> pointSeries21;
		public List<Vector2> pointSeries22;
		public List<Vector2> pointSeries23;
		public List<Vector2> pointSeries24;
		public List<Vector2> pointSeries25;

        public List<Vector2> pointSeries28;
        public List<Vector2> pointSeries29;

		private float H1, H2, H3, H4, H5;
		//-------DRAFT VARIABLE
		private float Draft_1P,
				Draft_2P,
				Draft_3P;
		private float Draft_1SB,
				Draft_2SB,
				Draft_3SB;


		//--------------CARGO TANK for sensor	input
		private float Hold1_1FCTR ;
		private float Hold1_1S ;
		private float Hold1_2P ;
		private float Hold1_2S ;
		private float Hold2_1P ;
		private float Hold2_1S ;
		private float Hold2_2P ;
		private float Hold2_2S ;
		private float Hold3_1P;
		private float Hold3_1S ;
		private float Hold3_2P ;
		private float Hold3_2S;
		private float Hold4_1P ;
		private float Hold4_1S ;
		private float Hold4_2P ;
		private float Hold4_2S ;
		private float Hold5_1P ;
		private float Hold5_1S ;
		private float Hold5_2BCTR ;
		private float Hold5_2S ;
		private float D1P;
		private float D2P;
		private float D1S;
		private float D2S;
		float btnHeight = 35;
		float btWidth = 100;

        public GameObject G_point;

    //MANUAL INPUT
        private float vH1_p, vH2_p, vH3_p, vH4_p, vH5_p;
        private float vH1_s, vH2_s, vH3_s, vH4_s, vH5_s;

 
		//------------------------------------------
		//---SIDE TANK water ballast
		private float ST_1P = 0.0f;
		private float ST_1SB = 0.0f;
		private float ST_2P = 0.0f;
		private float ST_2SB = 0.0f;
		private float ST_3P = 0.0f;
		private float ST_3SB = 0.0f;
		private float ST_4P = 0.0f;
		private float ST_4SB = 0.0f;
		private float ST_5P = 0.0f;
		private float ST_5SB = 0.0f;

        private float VWBF = 0.0f;
        private float VAWBP = 0.0f;
        private float VAWBSB = 0.0f;
        private float VAWBLO = 0.0f;
		

		//---DOUBLE BOTTOM

		private float DB_1P = 0.0f;
		private float DB_2P = 0.0f;
		private float DB_3P = 0.0f;
		private float DB_4P = 0.0f;
		private float DB_5P = 0.0f;
		private float DB_1SB = 0.0f;
		private float DB_2SB = 0.0f;
		private float DB_3SB = 0.0f;
		private float DB_4SB = 0.0f;
		private float DB_5SB = 0.0f;


        public float Gy;

        //WMG_Series w24 = GameObject.Find("sShipForm").GetComponent<WMG_Series>();

		
		//----------posisi form
        private Rect frmInput = new Rect(Screen.width * 1.7f / 2 - (Screen.width / 2), Screen.height * 2.43f / 2 - (Screen.height / 2), 480, 200);

        private Rect frmCalib = new Rect(Screen.width * 2.21f / 2 - (Screen.width / 2), Screen.height * 1.26f / 2 - (Screen.height / 2), 550, 800);
        private Rect frmSimulasi = new Rect(Screen.width * 1.6f / 2 - (Screen.width / 2), Screen.height * 1.26f / 2 - (Screen.height / 2), 530, 600);
        private Rect frmChart = new Rect(Screen.width / 2 - (Screen.width * 0.74f / 2), Screen.height / 2 - (Screen.height * 0f / 2), 1120, 300);
        private Rect frmMode = new Rect(Screen.width * 1.88f / 2 - (Screen.width / 2), Screen.height * 1.26f / 2 - (Screen.height / 2), 230, 83);
        private Rect frmExit = new Rect(Screen.width * 2.58f / 2 - (Screen.width / 2), Screen.height * 1.26f / 2 - (Screen.height / 2), 230, 83);
        private Rect frmHelp = new Rect(Screen.width * 2.32f / 2 - (Screen.width / 2), Screen.height * 1.26f / 2 - (Screen.height / 2), 350, 200);
		//-----------------------------------

		public GameObject shipMode;//
		public Transform shipVessel;
		public Material shader1;//shader solid
		public Material shader2;//shader transparan

		//-----------beban
	
		private float maxW = 1.0f;
		private float minW = 0.0f;
		private float wBeban = -200;
		//-------------------------------------
		private float maxAngle = 5.00f;
		private float minAngle = 0.00f;



		// BALLAST MODE SELECTOR
		public string[] BallastModeItems;
		public Rect Box;
		public string BallastselectedItem = "Manual";
		private bool editing = false;
		private int BallastMode = 0;
		private int ballastModeData;
		//-----------------------------------
		//---ARM FORCE
		private float AF1P;
		private float AF2P;
		private float AF1SB;
		private float AF2SB;
	
		//------------------------------------------
	
		//------level akuarium
		private float lvAkuariumF;
		private float lvAkuariumB;


		//-------------draft

		private float maxIn = 5.0f;
		private float minIn = 0.0f;
		private float tDraft;


		//----------------------------

		//tray 

		private float Tray1, Tray2, Tray3, Tray4, Tray5;


		//---------------
		
		private float sldTrim ;
		private float sldList ;
		private float draft;
		private string btnSimulation = "Simulation";
		private bool menuSim = false;
		private bool menuCalib = false;
		private bool menuMode = false;
		private bool menuExit = false;
		private bool menuChart = false;
		private bool menu2dShip = false;
		private bool menuBallastMode = false; // MENU BALLAST MODE
		public bool menuInput = false;
		private bool menuChartS = false;
        private bool menuHelp = false;

		private bool tabTrimHold = true;
		private bool tabBallast = false;
		private bool tabDraft = false;
		private bool tabAF = false;
		private bool tabLvAqua = false;
		private float level;
		private float lvDraft;
		private float draftTotal;
		private float KG, KM, KB, KB_rot;
		private int count;
		private float[] tempAI = new float[3];
		public GameObject cam2D;
		public GameObject camChart;
		public GameObject port;
		//float kgData; // KG Data

		private float[] xShip;
		private float[] yShip;
		private float[] xKB;
		private float[] yKB;
		private float[] xKM;
		private float[] yKM;
		private float[] xKG;
		private float[] yKG;
		private float[] xWL;
		private float[] yWL;
		private float buttonW = 75, buttonH = 35;
		private float posX = 200;
        public StreamWriter writer;


        //"Sebaran muatan kapal model dan CG"
        float _posVG;//posisi Vertikal G
        float _mTotal;//jumlah total mutan
        float _Gmx, _Gmy, _Gmz; //pusat massa total muatan
        public float _BobotTotal;

        public float _Gx1M, _Gy1M, _Gz1M,
                _Gx0, _Gy0, _Gz0;//titik G dalam keadaan kosong

        float _bobotAwal = 41.0f;//bobot awal saat kapal kosong
        public float total_Arm;

        float _Gx1, _Gx2, _Gx3, _Gx4, _Gx5,
              _Gy1, _Gy2, _Gy3, _Gy4, _Gy5,
               _Gz1, _Gz2, _Gz3, _Gz4, _Gz5;

        float _v1, _v2, _v3, _v4, _v5;

        float _h1, _h2, _h3, _h4, _h5;//tinggi muatan

        float _m1, _m2, _m3, _m4, _m5;//bobot muatan
           
        float mCOT1P, mCOT2P, mCOT3P, mCOT4P, mCOT5P;
        float mCOT1S, mCOT2S, mCOT3S, mCOT4S, mCOT5S;


        float p = 1;//kerapatan massa kg

        float _vol_CT1, _vol_CT2, _vol_CT3, _vol_CT4, _vol_CT5;

        float _heel;
        float _trim;

        private float Moment_Tray1 = 0, Moment_Tray2 = 0, Moment_Tray3 = 0, Moment_Tray4 = 0;
        private float GLoad_Tray1 = 0, GLoad_Tray2 = 0, GLoad_Tray3 = 0, GLoad_Tray4 = 0;
        private float GLoadTotalTransversal = 0;
        private float VoltPort1 = 0, VoltPort2 = 0, VoltPort3 = 0, VoltPort4 = 0,
                           VoltStar1 = 0, VoltStar2 = 0, VoltStar3 = 0, VoltStar4 = 0;
        private float VoltTotal = 0;

        public float temp_yCG;
        public float temp_zCG;
        public float Gmx;
        public float Gmy;
    

		//double[,] gzDataTable2D; // GZ table, 2D look-up table 
		////double[] kbDataTable1D;  // KB table, 1D look-up table
		////double[] kmDataTable1D;  // KM table, 1D look-up table
		//double[,] tcbDataTable2D;  // TCB table, 2D look-up table
		//double[,] kbDataTable2D;  // KB table, 2D look-up table
		//double[,] kmDataTable2D;  // KM table, 2D look-up table
		//double[] drDataTable1D;  // draught (draft), 1D look-up table
		//double[] heelDataVectorGZ; // heel angle, input vector for GZ
		//double[] dispDataVectorGZ; // displacement, input vector for GZ
		//double[] heelDataVectorKM; // heel angle, input vector for KM, KB, TCB
		//double[] dispDataVectorKM; // displacement, input vector for KM, KB, TCB
		//double[] heelDataVector; // heel angle, input vector
		//double[] dispDataVector; // displacement, input vector

		#region Graph Algoritma
		
		

        //pointd[] shippointslon = new pointd[21]; // longitudinal hull coordinate
        //pointd[] shippointslon_init = new pointd[21];
        //pointd[] shippointslon_BC = new pointd[21] {       // Bulk Carrier
        //    new pointd{x = -923.0,	y = 177.4}, // point 0
        //    new pointd{x = -923.0,	y = 111.6}, // point 1
        //    new pointd{x = -813.1,	y =  78.7}, // point 2
        //    new pointd{x = -813.1,	y =  20.1}, // point 3
        //    new pointd{x = -807.6,	y =  12.8}, // point 4
        //    new pointd{x = -798.5,	y =   5.5}, // point 5
        //    new pointd{x = -791.1,	y =   0.0}, // point 6
        //    new pointd{x =    0.0,	y =   0.0}, // point 7
        //    new pointd{x =  970.6,	y =   0.0}, // point 8
        //    new pointd{x =  996.3,	y =   7.3}, // point 9
        //    new pointd{x = 1012.7,	y =  22.0}, // point 10
        //    new pointd{x = 1027.4,	y =  36.6}, // point 11 
        //    new pointd{x = 1032.9,	y =  58.5}, // point 12
        //    new pointd{x = 1023.7,	y =  84.1}, // point 13
        //    new pointd{x =  981.6,	y = 118.9}, // point 14
        //    new pointd{x =  979.8,	y = 129.9}, // point 15
        //    new pointd{x =  981.6,	y = 135.4}, // point 16
        //    new pointd{x = 1016.4,	y = 201.2}, // point 17
        //    new pointd{x =  857.1,	y = 201.2}, // point 18
        //    new pointd{x =  835.1,	y = 177.4}, // point 19
        //    new pointd{x = -923.0,	y = 177.4}, // point 20
        //};
		

		//float dDisp = 0;
		//float dHeel = 0;
		//float dGZVal = 0;
		//float dKBVal = 0;
		//float dKMVal = 0;
		//float dTCBVal = 0;
		//float dDraftVal = 0;

        public float dDispVal = 0;   // displacement value in kgf
        public float dHeelVal = 0;   // heel angle in deg
        public float dTrimVal = 0;   // trim angle in deg, 20150822
        public float dGZVal = 0;     // GZ value in mm
        public float dKNVal = 0;     // KN value in mm
        public float dKBTVal = 0;    // KB transversal value in mm
        public float dKMTVal = 0;    // KM transversal value in mm
        public float dTCBVal = 0;    // CB transversal value in mm
        public float dDraftVal = 0;  // draft value in mm
        public float dInputVal = 0;  // Input value = Displacement or Draft
        public float dOutputVal = 0; // Output value = Displacement or Draft
        public float dPitchVal = 0;  // pitch angle in deg, pitch = -trim, 20140919

        public float dKBLVal = 0;    // KB longitudinal value in mm
        public float dKMLVal = 0;    // KM longitudinal value in mm
        public float dLCBVal = 0;    // CB longitudinal value in mm
        public float dLCFVal = 0;    // LCF longitudinal value in mm 
        public float dKCFVal = 0;    // KCF longitudinal value in mm 

		float scale_ld = 50;//0.05f; // scale for load distribution, graph drawing purpose
		float scale_sf = 200;//0.2f; // scale for shear force, graph drawing purpose
		float scale_bm = 500;//0.5f; // scale for bending moment, graph drawing purpose

		// calculate load

		private float dx;

		private void rotate_point (ref float px, ref float py, float cx, float cy, float angle_deg)
		{
				float s = Mathf.Sin (angle_deg * Mathf.PI / 180);
				float c = Mathf.Cos (angle_deg * Mathf.PI / 180);
            
				// translate point back to origin:
				px -= cx;
				py -= cy;

				// rotate point
				float xnew = px * c - py * s;
				float ynew = px * s + py * c;

				// translate point back:
				px = xnew + cx;
				py = ynew + cy;
		}
		
		#endregion

        #region Hydrostatic data of Bulk Carrier
        // heel angle (deg), new Bulk Carrier model scale 1:87, 20150827
        float[] heelData_BC = new float[8] { 0, 5, 10, 15, 20, 25, 30, 40 };

        // trim angle (deg), new Bulk Carrier model scale 1:87, 20150827
        float[] trimData_BC = new float[5] { -10, -5, 0, 5, 10 };

        // displacement data, new Bulk Carrier model scale 1:87, 20150827
        float[] dispData_BC = new float[14] { 2.71f, 5.83f, 12.40f, 19.17f, 26.03f, 32.94f, 39.92f, 46.96f, 54.09f, 61.31f, 68.69f, 76.23f, 80.08f, 83.99f };

        // draft data, new Bulk Carrier model scale 1:87, 20150827
        float[] draftData_BC = new float[14] { 6.125f, 12.250f, 24.500f, 36.750f, 49.000f, 61.250f, 73.500f, 85.750f, 98.000f, 110.250f, 122.501f, 134.751f, 140.875f, 147.000f };

        // new data, new Bulk Carrier model scale 1:87, 20150827
        public float KG_BC_REAL = 110.288f;  // mm from Keel (CATIA DATA, 20150821); 
        float LCG_BC = 1100.0f; // mm from AP (CATIA DATA, 20150821); 
        public float KG_BC_ORCA3D = 0.373f;  // mm from Keel (vertical position of Zero point in ORCA3D, 20150821); 

        // lookup table data : knData(row, column), new Bulk Carrier model scale 1:87, 20150827
        // row   = dispData_BC
        // colum = heelData_BC
        float[,] knData_BC = new float[14, 8] {
            {0.00f,	79.50f,	82.00f,	83.04f,	83.47f,	83.47f,	83.11f,	81.42f},
            {0.00f,	78.21f,	82.04f,	83.39f,	83.74f,	83.47f,	82.75f,	80.42f},
            {0.00f,	71.17f,	80.65f,	82.92f,	83.85f,	83.99f,	83.63f,	82.10f},
            {0.00f,	50.95f,	78.16f,	82.03f,	83.60f,	84.36f,	84.56f,	84.19f},
            {0.00f,	39.14f,	73.30f,	80.88f,	83.32f,	84.59f,	85.36f,	86.18f},
            {0.00f,	31.95f,	62.79f,	78.83f,	82.96f,	84.85f,	86.09f,	88.02f},
            {0.00f,	27.15f,	53.87f,	75.88f,	82.18f,	85.08f,	86.84f,	89.74f},
            {0.00f,	23.77f,	47.40f,	70.04f,	80.96f,	85.10f,	87.57f,	91.40f},
            {0.00f,	21.30f,	42.56f,	63.40f,	79.31f,	84.88f,	88.19f,	93.04f},
            {0.00f,	19.44f,	38.87f,	58.17f,	76.56f,	84.43f,	88.68f,	94.69f},
            {0.00f,	18.00f,	36.00f,	54.00f,	71.60f,	83.76f,	89.05f,	96.37f},
            {0.00f,	16.88f,	33.76f,	50.67f,	67.49f,	82.75f,	89.32f,	98.09f},
            {0.00f,	16.41f,	32.83f,	49.27f,	65.71f,	81.58f,	89.43f,	98.97f},
            {0.00f,	15.99f,	31.99f,	48.02f,	64.10f,	79.84f,	89.52f,	99.83f}
        };

        // lookup table data : tcbData(row, column), new Bulk Carrier model scale 1:87, 20150827
        // row   = dispData_BC
        // colum = heelData_BC
        float[,] tcbData_BC = new float[14, 8] {
            {0.00f,	-79.97f,	-84.51f,	-89.06f,	-94.37f,	-100.44f,	-107.13f,	-121.55f},
            {0.00f,	-78.36f,	-83.83f,	-88.29f,	-93.10f,	-98.48f,	    -104.41f,	-117.38f},
            {0.00f,	-70.79f,	-81.20f,	-85.81f,	-90.40f,	-95.34f,	    -100.74f,	-112.59f},
            {0.00f,	-50.20f,	-77.61f,	-83.15f,	-87.64f,	-92.43f,	    -97.57f,	    -108.81f},
            {0.00f,	-38.08f,	-71.78f,	-80.35f,	-85.06f,	-89.66f,	    -94.62f,	    -105.46f},
            {0.00f,	-30.58f,	-60.51f,	-76.79f,	-82.53f,	-87.12f,	    -91.88f,	    -102.39f},
            {0.00f,	-25.48f,	-50.92f,	-72.45f,	-79.72f,	-84.69f,	    -89.36f,	    -99.54f},
            {0.00f,	-21.81f,	-43.81f,	-65.44f,	-76.60f,	-82.19f,	    -86.97f,	    -96.88f},
            {0.00f,	-19.05f,	-38.35f,	-57.77f,	-73.17f,	-79.57f,	    -84.61f,	    -94.41f},
            {0.00f,	-16.90f,	-34.05f,	-51.54f,	-68.77f,	-76.85f,	    -82.24f,	    -92.11f},
            {0.00f,	-15.18f,	-30.58f,	-46.41f,	-62.40f,	-73.99f,	    -79.85f,	    -89.97f},
            {0.00f,	-13.75f,	-27.73f,	-42.12f,	-56.92f,	-70.88f,	    -77.45f,	    -87.99f},
            {0.00f,	-13.13f,	-26.48f,	-40.23f,	-54.47f,	-68.71f,	    -76.25f,	    -87.03f},
            {0.00f,	-12.56f,	-25.33f,	-38.50f,	-52.19f,	-66.08f,	    -75.06f,	    -86.10f}
        };


        // lookup table data : kbtData(row, column), KBT = KB transversal, new Bulk Carrier model scale 1:87, 20150827
        // row   = dispData_BC
        // colum = heelData_BC
        float[,] kbtData_BC = new float[14, 8] {
            {1.88f,	-1.74f,	-6.82f,	-11.39f,	-15.14f,	-17.83f,	-19.32f,	-18.21f},
            {3.52f,	1.71f,	-2.88f,	-7.19f,	-10.85f,	-13.61f,	-15.28f,	-14.77f},
            {6.84f,	7.46f,	3.95f,	0.12f,	-3.19f,	-5.71f,	-7.22f,	-6.46f},
            {10.13f,	10.80f,	9.96f,	6.64f,	3.63f,	1.39f,	0.12f,	1.31f},
            {13.38f,	13.93f,	15.02f,	12.63f,	9.93f,	7.88f,	6.81f,	8.40f},
            {16.61f,	17.07f,	18.44f,	17.99f,	15.81f,	13.96f,	13.03f,	14.93f},
            {19.83f,	20.22f,	21.45f,	22.75f,	21.24f,	19.72f,	18.92f,	21.02f},
            {23.05f,	23.39f,	24.49f,	26.41f,	26.26f,	25.13f,	24.54f,	26.79f},
            {26.29f,	26.59f,	27.58f,	29.38f,	30.89f,	30.24f,	29.89f,	32.33f},
            {29.56f,	29.83f,	30.71f,	32.39f,	34.93f,	35.08f,	35.02f,	37.71f},
            {32.88f,	33.12f,	33.92f,	35.47f,	37.94f,	39.68f,	39.96f,	42.99f},
            {36.26f,	36.49f,	37.22f,	38.64f,	40.99f,	44.03f,	44.77f,	48.23f},
            {37.98f,	38.20f,	38.90f,	40.27f,	42.55f,	45.87f,	47.13f,	50.82f},
            {39.72f,	39.93f,	40.61f,	41.92f,	44.14f,	47.43f,	49.47f,	53.39f}
        };


        // lookup table data : kmtData(row, column), KMT = KM transversal, new Bulk Carrier model scale 1:87, 20150827
        // row   = dispData_BC
        // colum = heelData_BC
        float[,] kmtData_BC = new float[14, 8] {
            {2464.31f,	362.02f,	330.87f,	309.86f,	288.82f,	266.17f,	242.25f,	195.92f},
            {1265.09f,	211.92f,	185.97f,	169.99f,	156.13f,	142.82f,	130.44f,	110.65f},
            {668.03f,	292.09f,	113.53f,	102.37f,	93.99f,	87.50f,	82.33f,	77.49f},
            {462.29f,	436.84f,	121.25f,	84.53f,	78.75f,	74.61f,	72.25f,	74.31f},
            {355.32f,	347.48f,	165.87f,	89.08f,	74.42f,	72.10f,	71.64f,	78.05f},
            {290.23f,	290.22f,	275.97f,	105.02f,	79.34f,	73.27f,	74.22f,	84.15f},
            {247.76f,	250.18f,	247.96f,	129.47f,	89.46f,	78.81f,	78.15f,	91.04f},
            {218.57f,	220.97f,	224.70f,	217.22f,	102.27f,	87.04f,	84.34f,	98.20f},
            {197.43f,	199.46f,	205.62f,	207.31f,	118.37f,	96.70f,	92.03f,	105.46f},
            {181.55f,	183.55f,	189.98f,	196.66f,	193.05f,	107.36f,	100.57f,	113.82f},
            {169.45f,	171.48f,	177.46f,	186.74f,	191.51f,	119.67f,	109.80f,	123.64f},
            {160.23f,	162.14f,	167.76f,	177.71f,	187.22f,	138.54f,	119.88f,	133.79f},
            {156.47f,	158.29f,	163.81f,	173.63f,	184.83f,	189.61f,	125.40f,	138.60f},
            {153.17f,	154.91f,	160.34f,	169.92f,	182.41f,	190.93f,	131.41f,	143.32f}
        };

        // lookup table data : lcbData(row, column), new Bulk Carrier model scale 1:87, 20150827
        // row = dispData_BC
        // colum = trimData_BC
        float[,] lcbData_BC = new float[14, 5] { 
            {-737.32f,	-665.33f,	49.18f,	807.07f,	888.79f},
            {-675.04f,	-574.68f,	52.39f,	724.26f,	819.56f},
            {-629.84f,	-467.76f,	57.72f,	616.27f,	725.52f},
            {-601.86f,	-399.58f,	62.50f,	535.71f,	660.53f},
            {-575.95f,	-350.99f,	66.54f,	468.36f,	611.39f},
            {-542.59f,	-313.19f,	69.59f,	411.10f,	572.05f},
            {-503.87f,	-282.79f,	71.26f,	363.47f,	539.03f},
            {-461.75f,	-258.00f,	71.37f,	324.68f,	503.51f},
            {-417.31f,	-238.04f,	69.82f,	294.10f,	459.76f},
            {-371.17f,	-221.61f,	66.41f,	270.41f,	413.96f},
            {-323.67f,	-203.83f,	60.96f,	251.95f,	367.15f},
            {-274.90f,	-182.37f,	53.97f,	236.71f,	319.56f},
            {-250.05f,	-170.26f,	50.07f,	228.40f,	295.48f},
            {-224.92f,	-157.22f,	45.99f,	218.28f,	271.21f}
        };

        // lookup table data : kblData(row, column), kbl = KB longitudinal, new Bulk Carrier model scale 1:87, 20150827
        // row = dispData_BC
        // colum = trimData_BC
        float[,] kblData_BC = new float[14, 5] {
            {25.45f,	16.44f,	3.31f,	17.83f,	28.02f},
            {34.29f,	21.38f,	6.47f,	22.94f,	34.80f},
            {50.37f,	29.35f,	12.80f,	30.39f,	43.94f},
            {62.09f,	36.26f,	19.10f,	36.19f,	51.75f},
            {70.96f,	42.56f,	25.37f,	41.13f,	59.03f},
            {76.84f,	48.37f,	31.62f,	45.68f,	65.93f},
            {80.89f,	53.90f,	37.87f,	50.23f,	72.46f},
            {83.85f,	59.28f,	44.13f,	55.00f,	77.53f},
            {86.13f,	64.65f,	50.43f,	60.10f,	80.60f},
            {87.98f,	70.03f,	56.76f,	65.57f,	83.06f},
            {89.63f,	75.14f,	63.16f,	71.38f,	85.28f},
            {91.20f,	79.92f,	69.64f,	77.43f,	87.46f},
            {92.00f,	82.23f,	72.92f,	80.40f,	88.60f},
            {92.84f,	84.51f,	76.22f,	83.25f,	89.79f}
        };

        // lookup table data : kmlData(row, column), kml = KM longitudinal, new Bulk Carrier model scale 1:87, 20150827
        // row = dispData_BC
        // colum = trimData_BC
        float[,] kmlData_BC = new float[14, 5] {
            {590.97f,	1342.99f,	39148.72f,	1564.21f,	606.43f},
            {1160.35f,	1639.40f,	20033.09f,	1850.51f,	690.98f},
            {1675.60f,	2279.06f,	10440.44f,	2200.33f,	808.88f},
            {1842.45f,	3101.82f,	7091.73f,	2466.29f,	980.37f},
            {1693.72f,	3716.14f,	5349.81f,	2722.72f,	1172.46f},
            {1396.71f,	4034.74f,	4307.46f,	2881.13f,	1362.37f},
            {1184.65f,	4164.48f,	3643.25f,	2903.11f,	1485.05f},
            {1029.44f,	4128.44f,	3191.98f,	2812.43f,	1133.72f},
            {905.94f,	3940.43f,	2873.78f,	2623.04f,	885.32f},
            {796.08f,	3571.11f,	2660.59f,	2445.58f,	778.72f},
            {691.89f,	2770.64f,	2533.52f,	2296.63f,	667.10f},
            {595.40f,	2060.79f,	2460.47f,	1998.75f,	553.74f},
            {547.42f,	1749.54f,	2436.43f,	1748.21f,	496.52f},
            {498.041f,	1468.06f,	2414.27f,	1316.52f,	438.43f}
        };

        // lookup table data : lcfData(row, column), lcf = CF longitudinal, new Bulk Carrier model scale 1:87, 20150827
        // row = dispData_BC
        // colum = trimData_BC
        float [,] lcfData_BC = new float[14, 5] {
            {-652.80f,	-557.99f,	52.82f,	712.90f,	813.75f},
            {-607.54f,	-445.13f,	57.50f,	600.83f,	712.47f},
            {-570.42f,	-313.94f,	67.08f,	449.50f,	584.23f},
            {-530.64f,	-242.34f,	75.03f,	330.74f,	504.04f},
            {-464.20f,	-190.28f,	80.14f,	233.47f,	446.91f},
            {-369.46f,	-153.67f,	81.01f,	162.78f,	402.79f},
            {-272.25f,	-126.41f,	76.42f,	117.97f,	358.99f},
            {-173.90f,	-110.89f,	66.73f,	95.37f,	237.38f},
            {-75.31f,	-103.03f,	51.56f,	91.77f,	119.07f},
            {22.74f,	    -87.12f,	    29.02f,	95.31f,	24.25f},
            {119.93f,	-23.23f,	    2.64f,	101.78f,	-67.95f},
            {217.52f,	50.01f,	    -21.54f,	79.18f,	-158.67f},
            {266.14f,	89.45f,	    -32.59f,	44.16f,	-203.77f},
            {314.40f,    130.99f,	    -42.36f,	-28.19f,	-248.82f}
        };

        // lookup table data : kcfData(row, column), kcf = KF longitudinal, new Bulk Carrier model scale 1:87, 20150827
        // row = dispData_BC
        // colum = trimData_BC
        float[,] kcfData_BC = new float[14, 5] {
            {33.51f,	21.48f,	6.13f,	23.36f,	35.91f},
            {51.15f,	29.55f,	12.25f,	30.98f,	44.78f},
            {75.38f,	42.78f,	24.50f,	42.31f,	59.09f},
            {90.84f,	54.85f,	36.75f,	51.02f,	72.85f},
            {98.18f,	65.26f,	49.00f,	58.76f,	85.78f},
            {99.61f,	75.17f,	61.25f,	67.06f,	97.82f},
            {100.35f,	84.82f,	73.50f,	76.61f,	107.72f},
            {100.85f,	94.85f,	85.75f,	87.59f,	103.44f},
            {101.44f,	105.19f,	98.00f,	100.03f,	100.69f},
            {102.44f,	114.71f,	110.25f,	113.04f,	102.41f},
            {104.24f,	120.49f,	122.50f,	126.32f,	105.27f},
            {106.95f,	126.41f,	134.75f,	137.25f,	109.64f},
            {108.84f,	129.62f,	140.88f,	141.02f,	112.58f},
            {111.26f,	133.06f,	147.00f,	142.07f,	116.15f}
        };
        #endregion 
        struct pointd
        {
            public float x, y;
        }
        pointd[] shippointslon = new pointd[21]; // longitudinal hull coordinate
        pointd[] shippointslon_init = new pointd[21];
        pointd[] shippointslon_BC = new pointd[21] {       // Bulk Carrier
            new pointd{x = -923.0f,	y = 177.4f}, // point 0
            new pointd{x = -923.0f,	y = 111.6f}, // point 1
            new pointd{x = -813.1f,	y =  78.7f}, // point 2
            new pointd{x = -813.1f,	y =  20.1f}, // point 3
            new pointd{x = -807.6f,	y =  12.8f}, // point 4
            new pointd{x = -798.5f,	y =   5.5f}, // point 5
            new pointd{x = -791.1f,	y =   0.0f}, // point 6
            new pointd{x =    0.0f,	y =   0.0f}, // point 7
            new pointd{x =  970.6f,	y =   0.0f}, // point 8
            new pointd{x =  996.3f,	y =   7.3f}, // point 9
            new pointd{x = 1012.7f,	y =  22.0f}, // point 10
            new pointd{x = 1027.4f,	y =  36.6f}, // point 11 
            new pointd{x = 1032.9f,	y =  58.5f}, // point 12
            new pointd{x = 1023.7f,	y =  84.1f}, // point 13
            new pointd{x =  981.6f,	y = 118.9f}, // point 14
            new pointd{x =  979.8f,	y = 129.9f}, // point 15
            new pointd{x =  981.6f,	y = 135.4f}, // point 16
            new pointd{x = 1016.4f,	y = 201.2f}, // point 17
            new pointd{x =  857.1f,	y = 201.2f}, // point 18
            new pointd{x =  835.1f,	y = 177.4f}, // point 19
            new pointd{x = -923.0f,	y = 177.4f}, // point 20
        };
        pointd[] shippoints = new pointd[13];
        pointd[] shippoints_init = new pointd[13];
        pointd[] shippoints_BC = new pointd[13] { 
            new pointd{x = -158.43f,	y =   74.85f}, // point 0
            new pointd{x = -158.43f,	y =  -82.62f}, // point 1
            new pointd{x = -156.54f,	y =  -89.12f}, // point 2
            new pointd{x = -153.51f,	y =  -93.45f}, // point 3
            new pointd{x = -148.74f,	y =  -99.07f}, // point 4
            new pointd{x = -137.47f,	y = -102.12f}, // point 5
            new pointd{x =       0.0f,	y = -102.12f}, // point 6
            new pointd{x =  137.47f,	y = -102.12f}, // point 7
            new pointd{x =  148.74f,	y =  -99.07f}, // point 8
            new pointd{x =  153.51f,	y =  -93.45f}, // point 9
            new pointd{x =  156.54f,	y =  -89.12f}, // point 10
            new pointd{x =  158.43f,	y =  -82.62f}, // point 11 
            new pointd{x =  158.43f,	y =   74.85f}, // point 12
        };

        // longitudinal position for strength curve calculation, Bulk Carrier Ship (BC)
        pointd[] lonpos_BC = new pointd[33] { 
            new pointd{x =    0.00f,	y = 0}, // point 0
            new pointd{x =  193.75f,	y = 0}, // point 1
            new pointd{x =  387.50f,	y = 0}, // point 2
            new pointd{x =  387.50f,	y = 0}, // point 3  --> compartment 5
            new pointd{x =  532.50f,	y = 0}, // point 4  --> compartment 5
            new pointd{x =  677.50f,	y = 0}, // point 5  --> compartment 5
            new pointd{x =  677.50f,	y = 0}, // point 6
            new pointd{x =  702.65f,	y = 0}, // point 7
            new pointd{x =  727.80f,	y = 0}, // point 8  
            new pointd{x =  727.80f,	y = 0}, // point 9  --> compartment 4 
            new pointd{x =  827.80f,	y = 0}, // point 10 --> compartment 4 
            new pointd{x =  927.80f,	y = 0}, // point 11 --> compartment 4
            new pointd{x =  927.80f,	y = 0}, // point 12 
            new pointd{x =  950.30f,	y = 0}, // point 13
            new pointd{x =  972.80f,	y = 0}, // point 14
            new pointd{x =  972.80f,	y = 0}, // point 15 --> compartment 3
            new pointd{x = 1107.30f,	y = 0}, // point 16 --> compartment 3 
            new pointd{x = 1241.80f,	y = 0}, // point 17 --> compartment 3 
            new pointd{x = 1241.80f,	y = 0}, // point 18
            new pointd{x = 1266.30f,	y = 0}, // point 19 
            new pointd{x = 1290.80f,	y = 0}, // point 20
            new pointd{x = 1290.80f,	y = 0}, // point 21 --> compartment 2
            new pointd{x = 1399.80f,	y = 0}, // point 22 --> compartment 2 
            new pointd{x = 1508.80f,	y = 0}, // point 23 --> compartment 2
            new pointd{x = 1508.80f,	y = 0}, // point 24
            new pointd{x = 1529.80f,	y = 0}, // point 25 
            new pointd{x = 1550.80f,	y = 0}, // point 26
            new pointd{x = 1550.80f,	y = 0}, // point 27 --> compartment 1 
            new pointd{x = 1645.80f,	y = 0}, // point 28 --> compartment 1 
            new pointd{x = 1740.80f,	y = 0}, // point 29 --> compartment 1  
            new pointd{x = 1740.80f,	y = 0}, // point 30 
            new pointd{x = 1825.80f,	y = 0}, // point 31
            new pointd{x = 1910.80f,	y = 0}, // point 32
        };


        //float dDispVal = 0;   // displacement value in kgf
        //float dHeelVal = 0;   // heel angle in deg
        //float dTrimVal = 0;   // trim angle in deg, 20150822
        //float dGZVal = 0;     // GZ value in mm
        //float dKNVal = 0;     // KN value in mm
        //float dKBTVal = 0;    // KB transversal value in mm
        //float dKMTVal = 0;    // KM transversal value in mm
        //float dTCBVal = 0;    // CB transversal value in mm
        //float dDraftVal = 0;  // draft value in mm
        //float dInputVal = 0;  // Input value = Displacement or Draft
        //float dOutputVal = 0; // Output value = Displacement or Draft
        //float dPitchVal = 0;  // pitch angle in deg, pitch = -trim, 20140919

        //float dKBLVal = 0;    // KB longitudinal value in mm
        //float dKMLVal = 0;    // KM longitudinal value in mm
        //float dLCBVal = 0;    // CB longitudinal value in mm
        //float dLCFVal = 0;    // LCF longitudinal value in mm 
        //float dKCFVal = 0;    // KCF longitudinal value in mm 

        #region Interpolation algorithm for lookup table 2D

		// global variables
		int N = 0; // number of row
		int M = 0; // number of column

		int i = 0;
		int j = 0;
		float zA = 0.0f;
		float zB = 0.0f;
		float zC = 0.0f;
		float zD = 0.0f;

		// float dx = 0.0f;
		float dy = 0.0f;
		float delx = 0.0f;
		float dely = 0.0f;

		private float Interpolate2D (float xs, float ys, float[] x, float[] y, float[,] z)
		{
				N = x.Length;
				M = y.Length;
				int k = 0;
				float zs = 0;

				// first check for input x
				if (xs < x [0]) {  // case 0
						i = 0;
				} else if (xs > x [N - 1]) { // case 1
						i = N - 2;
				} else { // case 2
						for (k = 0; k < N - 1; k++) {
								if (xs < x [k]) {
										break;
								}
						}
						i = k - 1;
				}

				// first check for input y
				if (ys < y [0]) {  // case 0
						j = 0;
				} else if (ys > y [M - 1]) { // case 1
						j = M - 2;
				} else { // case 2
						for (k = 0; k < M - 1; k++) {
								if (ys < y [k]) {
										break;
								}
						}
						j = k - 1;
				}

				zA = z [i, j];
				zB = z [i, j + 1];
				zC = z [i + 1, j + 1];
				zD = z [i + 1, j];

				dx = xs - x [i];
				dy = ys - y [j];
				delx = x [i + 1] - x [i];
				dely = y [j + 1] - y [j];

				zs = zA + dy / dely * (zB - zA) + dx / delx * (zD - zA) + dx / delx * dy / dely * (zA + zC - zB - zD);

				return zs;
		}

        #endregion

        #region Interpolation algorithm for lookup table 1D

		private float Interpolate1D (float xs, float[] x, float[] z)
		{
				N = x.Length;
				int k = 0;
				float zs = 0;

				// first check for input x
				if (xs < x [0]) {  // case 0
						i = 0;
				} else if (xs > x [N - 1]) { // case 1
						i = N - 2;
				} else { // case 2
						for (k = 0; k < N - 1; k++) {
								if (xs < x [k]) {
										break;
								}
						}
						i = k - 1;
				}

				zA = z [i];
				zB = z [i + 1];

				dx = xs - x [i];
				delx = x [i + 1] - x [i];

				zs = zA + dx / delx * (zB - zA);

				return zs;
		}

		// tutorial in array c#
		// http://msdn.microsoft.com/en-us/library/aa288453%28v=vs.71%29.aspx

        #endregion


        private WMG_Series wPoint0, wPoint1, wPoint2, wPoint3, wPoint4, wPoint5
                           , wPoint6, wPoint7, wPoint8, wPoint9, wPoint10, wPoint11;

        private WMG_Series wPoint12, wPoint13, wPoint14, wPoint15, wPoint16, wPoint17, wPoint18, wPoint19, wPoint20;

        private WMG_Series wPoint21, wPoint22, wPoint23, wPoint24, wPoint25, wPoint26, wPoint27, wPoint28, wPoint29;

    float[,] gzDataTable2D;   // GZ table, 2D look-up table 
    float[,] knDataTable2D;   // KN table, 2D look-up table 
    float[,] tcbDataTable2D;  // TCB table, transversal CB, 2D look-up table
    float[,] lcbDataTable2D;  // LCB table, longitudinal CB, 2D look-up table
    float[,] kbtDataTable2D;  // KBT table, transversal KB, 2D look-up table
    float[,] kblDataTable2D;  // KBL table, longitudinal KB, 2D look-up table
    float[,] kmtDataTable2D;  // KMT table, transversal KM, 2D look-up table
    float[,] kmlDataTable2D;  // KML table, longitudinal KM, 2D look-up table
    float[,] kcfDataTable2D;  // KCF table, longitudinal KF, 2D look-up table
    float[,] lcfDataTable2D;  // LCF table, longitudinal CF, 2D look-up table
    float[] draftDataTable1D; // draught (draft), 1D look-up table (1D Table = Vector)
    float[] heelDataTable1D;  // heel angle, 1D look-up table (1D Table = Vector)
    float[] dispDataTable1D;  // displacement, 1D look-up table (1D Table = Vector)
    float[] trimDataTable1D;  // trim angle, 1D look-up table (1D Table = Vector)

    float kgDataOrca3D; // KG Data at ORCA3D,  
    // kgDataOrca3D is vertical position of Zero Point at ORCA3D measured from Keel
    // this is the center point of rotation
    float kgDataReal;   // KG Data real, the real G point of ship 

        void Awake()
        {

         

            wPoint0 = GameObject.Find("s0-Ship").GetComponent<WMG_Series>();
            wPoint1 = GameObject.Find("s1-WL").GetComponent<WMG_Series>();
            wPoint2 = GameObject.Find("s2-KM").GetComponent<WMG_Series>();
            wPoint3 = GameObject.Find("s3-MB_line").GetComponent<WMG_Series>();
            wPoint4 = GameObject.Find("s4-GZ_line").GetComponent<WMG_Series>();
            wPoint5 = GameObject.Find("s5-KN_line").GetComponent<WMG_Series>();
            wPoint6 = GameObject.Find("s6-G_point").GetComponent<WMG_Series>();
            wPoint7 = GameObject.Find("s7-B_point").GetComponent<WMG_Series>();
            wPoint8 = GameObject.Find("s8-M_point").GetComponent<WMG_Series>();
            wPoint9 = GameObject.Find("s9-Z_point").GetComponent<WMG_Series>();
            wPoint10 = GameObject.Find("s10-K_point").GetComponent<WMG_Series>();
            wPoint11 = GameObject.Find("s11-N_point").GetComponent<WMG_Series>();
           
            wPoint25 = GameObject.Find("s12-G0").GetComponent<WMG_Series>();
            wPoint26 = GameObject.Find("s13-Gm").GetComponent<WMG_Series>();

            wPoint12 = GameObject.Find("s0-Ship_L").GetComponent<WMG_Series>();
            wPoint13 = GameObject.Find("s1-WL_L").GetComponent<WMG_Series>();
            wPoint14 = GameObject.Find("s2-WL_L").GetComponent<WMG_Series>();
            wPoint15 = GameObject.Find("s3-KM_line_L").GetComponent<WMG_Series>();
            wPoint16 = GameObject.Find("s4-BM_line_L").GetComponent<WMG_Series>();
            wPoint17 = GameObject.Find("s5-G_point_L").GetComponent<WMG_Series>();
            wPoint18 = GameObject.Find("s6-B_point_L").GetComponent<WMG_Series>();
            wPoint19 = GameObject.Find("s7-M_point_L").GetComponent<WMG_Series>();
            wPoint20 = GameObject.Find("s8-COF_point_L").GetComponent<WMG_Series>();

            wPoint27 = GameObject.Find("s9-G0").GetComponent<WMG_Series>();
            wPoint28 = GameObject.Find("s10-Gm").GetComponent<WMG_Series>();


            //wPoint21 = GameObject.Find("sLoad").GetComponent<WMG_Series>();
            //wPoint22 = GameObject.Find("sShear").GetComponent<WMG_Series>();
            //wPoint23 = GameObject.Find("sBend").GetComponent<WMG_Series>();
            //wPoint24 = GameObject.Find("sShipForm").GetComponent<WMG_Series>();
			//wPoint25 = GameObject.Find("s12-G_point 1").GetComponent<WMG_Series>();


            //data = GetComponent<UnitySerialPort>();
            
       

          //  sTank_1P.AIDataA[0];



            InitShipDataS();




        }

  



        void Start ()
		{
            

				shipMode = GameObject.FindWithTag ("lambung");
				
               
                data = UnitySerialPort.Instance;
             
              

				//lblCargoTank.SetActive(false);
				//lblSideTank.SetActive(false);

				cam2D.SetActive(false);
				camChart.SetActive(false);

				port.SetActive(false);

	
		
				waterBallastF ();
				waterTankF ();

				xWL = new float[2];
				yWL = new float[2];

				sldTrim = 2.5f;
				sldList = 2.5f;


                Draft_1P = 2.5f;
                Draft_2P = 2.5f;
                Draft_1SB = 2.5f;
                Draft_2SB = 2.5f;


				// initialize amidship points, then translate each point 
				//shippoints_init = (pointd[])shippoints_CO.Clone();
				//for (int i = 0; i < shippoints_init.Length; i++)
				//{
				//    shippoints_init[i].y += kgData;
				//}
				
               

				count = 0;

                //FileInfo theSourceFile = new FileInfo(Application.dataPath + "/" + "Test.txt");
                //StreamReader reader = theSourceFile.OpenText();

                //string text;

                //do
                //{
                //    text = reader.ReadLine();

                //    if (text != null)
                //    {

                //        string[] entries = text.Split('-');
                //        data_tes = entries[0].Split(',');

                //        for (int i = 0; i < text.Length; i++)
                //        {
                //           // print(CallData[1]);
                           
                //        }
                //        TrimCall = float.Parse(CallData[0]);
                //        ListCall = float.Parse(CallData[1]);
                //        draftCall = float.Parse(CallData[2]);


                //    }

                //} while (text != null);

                #region Initial G
                _Gx1 = 1888.86f;
                _Gx2 = 1614.50f;
                _Gx3 = 1277.20f;
                _Gx4 = 950.50f;
                _Gx5 = 627.86f;

                _Gy1 = 0;
                _Gy2 = 0;
                _Gy3 = 0;
                _Gy4 = 0;
                _Gy5 = 0;

                _Gx0 = 0;
                _Gy0 = 0;
                _Gz0 = 0;

                _mTotal = 0;

              


                #endregion

                //H1 = Hold1_1FCTR + Hold1_1S + Hold1_2P + Hold1_2S;
                //H2 = Hold2_1P + Hold2_1S + Hold2_2P + Hold2_2S;
                //H3 = Hold3_1P + Hold3_1S + Hold3_2P + Hold3_2S;
                //H4 = Hold4_1P + Hold4_1S + Hold4_2P + Hold4_2S;
                //H5 = Hold5_1P + Hold5_1S + Hold5_2BCTR + Hold5_2S;


              

               

		}


    //volume cargo
        public float Vol_Cargo1(float lvlCOT)//volume tangki
        {

            _vol_CT1 = level_Cargo1(lvlCOT) * 0.0411f * 10;

            return _vol_CT1;
        }
        public float Vol_Cargo2(float lvlCOT)//volume tangki
        {

            _vol_CT2 = level_Cargo2(lvlCOT) * 0.0687f * 10;

            return _vol_CT2;
        }
        public float Vol_Cargo3(float lvlCOT)//volume tangki
        {

            _vol_CT3 = level_Cargo3(lvlCOT) * 0.0850f * 10;

            return _vol_CT3;
        }
        public float Vol_Cargo4(float lvlCOT)//volume tangki
        {

            _vol_CT4 = level_Cargo4(lvlCOT) * 0.0638f * 10;

            return _vol_CT4;
        }
        public float Vol_Cargo5(float lvlCOT)//volume tangki
        {

            _vol_CT5 = level_Cargo5(lvlCOT) * 0.0808f * 10;

            return _vol_CT5;
        }



    //level cargo
        public float level_Cargo1(float v_in)//rumus untuk menentukan level sensor
        {

            level = 11.309f * v_in - 10.743f;

            return level;

        }
        public float level_Cargo2(float v_in)//rumus untuk menentukan level sensor
        {

            level = 11.408f * v_in - 12.726f;

            return level;

        }
        public float level_Cargo3(float v_in)//rumus untuk menentukan level sensor
        {

            level = 11.112f * v_in - 8.7253f;

            return level;

        }
        public float level_Cargo4(float v_in)//rumus untuk menentukan level sensor
        {

            level = 11.746f * v_in - 13.053f;

            return level;

        }
        public float level_Cargo5(float v_in)//rumus untuk menentukan level sensor
        {

            level = 11.404f * v_in - 11.095f;

            return level;

        }

    public void ModeKapal()
    {


        ModeMaterial = !ModeMaterial;

    }


    void Update ()
		{
        // InitialPoint();

        if (ModeMaterial == true)
        {
            modeTransparan();
        }
        else if (ModeMaterial == false)
        {
            modeNormal();
        }


        //DataAI ();//data ai
        // print("Muatan cargo 5 :" + _m5.ToString("F2"));

       //fromAI();
      // RollPitch();
        waterTankT();
        DraftSim();
        //CargoHold();
       
       

        //activeCargoTank();

        CalculateStrengthCurve_CO();
        CalculateTranverseHydrostatic();
        CalculateLongHydrostatic();
        CalculateCG_and_Attitude();
        PosBebanTray1();
        PosBebanTray2();
        PosBebanTray3();
        PosBebanTray4();



        #region auto ballas
        //	if (data != null) {
        //if (data.SerialPort != null) {
        //		if (data.SerialPort.IsOpen) {
        //
        //			data.SendSerialDataAsLine ("#," + BallastMode + ",0,0,0,0,$");
        //			print ("#," + BallastMode + ",0,0,0,0,$");
        //			//data.SendSerialDataAsLine("#," + BallastMode + ",0,0,0,$");
        //		}
        //}
        //}
        #endregion

    }

    void SaveData()
        {
            writer = new StreamWriter("My text.txt");
            writer.WriteLine("Hold 1p : " + vH1_s.ToString());
            writer.Flush();
        }

		
		void ClearPointT ()//clear point transversal
		{

				pointSeries0.Clear ();
				pointSeries1.Clear ();
				pointSeries2.Clear ();
				pointSeries3.Clear ();
				pointSeries4.Clear ();
				pointSeries5.Clear ();
				pointSeries6.Clear ();
				pointSeries7.Clear ();
				pointSeries8.Clear ();
				pointSeries9.Clear ();
				pointSeries10.Clear ();
				pointSeries11.Clear ();
                pointSeries25.Clear();
                pointSeries26.Clear();



		}

		void ClearPointL ()//clear point longitudinal
		{

				pointSeries12.Clear ();
				pointSeries13.Clear ();
				pointSeries14.Clear ();
				pointSeries15.Clear ();
				pointSeries16.Clear ();
				pointSeries17.Clear ();
				pointSeries18.Clear ();
				pointSeries19.Clear ();
				pointSeries20.Clear ();
                pointSeries27.Clear();
                pointSeries28.Clear();

		}

		void ClearPointS ()//clear point longitudinal
		{
				pointSeries21.Clear ();
				pointSeries22.Clear ();
				pointSeries23.Clear ();
				pointSeries24.Clear ();
		}

        #region CG muatan berdasarkan sensor 20150830

        public void PosBebanTray1()
        {

            VoltPort1 = Hold1_1FCTR + Hold1_2P;
            VoltPort1 -= 0.186f;

            if (VoltPort1 < 0) { VoltPort1 = 0; }

            VoltStar1 = Hold1_1S + Hold1_2S;
            VoltStar1 = VoltStar1 - 0.03f;

            if (VoltStar1 < 0) { VoltStar1 = 0; }

            Moment_Tray1 = 97.5f * VoltPort1 - 97.5f * VoltStar1;

            float tVoltPS = VoltPort1 + VoltStar1;

            if (tVoltPS > 0)
            {
                GLoad_Tray1 = Moment_Tray1 / (VoltPort1 + VoltStar1);
            }
            else
            {
                GLoad_Tray1 = 0;
            }

            print("moment T1 :" + Moment_Tray1.ToString("F2"));
            print("G Load T1 :" + GLoad_Tray1.ToString("F2"));
        }

        public void PosBebanTray2()
        {

            VoltPort2 = Hold2_1P + Hold2_2P;
            VoltPort2 -= 0.217f;

            if (VoltPort2 < 0) { VoltPort2 = 0; }

            VoltStar2 = Hold2_1S + Hold2_2S;
            VoltStar2 = VoltStar2 - 0.22f;


            if (VoltStar2 < 0) { VoltPort2 = 0; }

            float tVoltPS = VoltPort2 + VoltStar2;


            Moment_Tray2 = 97.5f * VoltPort2 - 97.5f * VoltStar2;

            if (tVoltPS > 0)
            {
                GLoad_Tray2 = Moment_Tray2 / (VoltPort2 + VoltStar2);
            }
            else
            {
                GLoad_Tray2 = 0;
            }




        }

        public void PosBebanTray3()
        {

            VoltPort3 = Hold3_1P + Hold3_2P;
            VoltPort3 -= 0.411f;

            if (VoltPort3 < 0) { VoltPort3 = 0; }

            VoltStar3 = Hold3_1S + Hold3_2S;
            VoltStar3 = VoltStar3 - 0.23f;


            if (VoltStar3 < 0) { VoltPort3 = 0; }

            float tVoltPS = VoltPort3 + VoltStar3;


            Moment_Tray3 = 97.5f * VoltPort3 - 97.5f * VoltStar3;

            if (tVoltPS > 0)
            {
                GLoad_Tray3 = Moment_Tray3 / (VoltPort3 + VoltStar3);
            }
            else
            {
                GLoad_Tray3 = 0;
            }

        }

        public void PosBebanTray4()
        {

            float VoltPort4 = Hold4_1P + Hold4_2P;
            VoltPort4 -= 0.02f;

            if (VoltPort4 < 0) { VoltPort4 = 0; }

            float VoltStar4 = Hold4_1S + Hold4_2S;
            VoltStar4 = VoltStar4 - 0.065f;

            if (VoltStar4 < 0) { VoltPort4 = 0; }

            float tVoltPS = VoltPort4 + VoltStar4;


            Moment_Tray4 = 97.5f * VoltPort4 - 97.5f * VoltStar4;

            if (tVoltPS > 0)
            {
                GLoad_Tray4 = Moment_Tray4 / (VoltPort4 + VoltStar4);
            }
            else
            {
                GLoad_Tray4 = 0;
            }



        }



        #endregion

        public void CalculateTranverseHydrostatic()
        {
            ChangePointHydrostatic();
            Rumus();

            float tempDisp;
            float tempList;
            float tempDraft;

            float _mTotal = Tray1 + Tray2 + Tray3 + Tray4 + Tray5;



            if (menuInput == true)
            {
                tempDisp = dWeightTotalShip;
                tempList = _heel;
                tempDraft = Interpolate1D(dDispVal, dispDataTable1D, draftDataTable1D);
                _BobotTotal = dWeightTotalLoad;
                temp_yCG = -yCGTotalLoad;
                temp_zCG = zCGTotalLoad;
            }
            else
            {
                fromAI();
                tempDisp = _mTotal + total_Arm + _bobotAwal;
                tempList = Konv_Sudut(sldList);
                tempDraft = tDraft + 50.0f;
                _BobotTotal = _mTotal + total_Arm;
                VoltTotal = VoltPort1 + VoltPort2 + VoltPort3 + VoltPort4 +
                        VoltStar1 + VoltStar2 + VoltStar3 + VoltStar4;

                if (VoltTotal > 0)
                {

                    GLoadTotalTransversal = (Moment_Tray1 + Moment_Tray2 + Moment_Tray3 + Moment_Tray4) / VoltTotal;
                }
                else
                {
                    GLoadTotalTransversal = 0;
                }
                temp_yCG = GLoadTotalTransversal;
                temp_zCG = KG_BC_REAL;
               
            }


            dDispVal = tempDisp; //total input dari slider
            dDraftVal = Interpolate1D(dDispVal, dispDataTable1D, draftDataTable1D); //tempDraft;
            //dOutput1 = dDraftVal;


            dHeelVal = tempList; 
            if (dHeelVal >= 0)
            {
                //dGZVal = Interpolate2D(dDispVal, dHeelVal, dispDataTable1D, heelDataTable1D, gzDataTable2D);
                dKNVal = Interpolate2D(dDispVal, dHeelVal, dispDataTable1D, heelDataTable1D, knDataTable2D);
                dTCBVal = Interpolate2D(dDispVal, dHeelVal, dispDataTable1D, heelDataTable1D, tcbDataTable2D);
            }
            else
            {
                //dGZVal = -1 * Interpolate2D(dDispVal, -dHeelVal, dispDataTable1D, heelDataTable1D, gzDataTable2D);
                dKNVal = -1 * Interpolate2D(dDispVal, -dHeelVal, dispDataTable1D, heelDataTable1D, knDataTable2D);
                dTCBVal = -1 * Interpolate2D(dDispVal, -dHeelVal, dispDataTable1D, heelDataTable1D, tcbDataTable2D);
            }

            dKBTVal = Interpolate2D(dDispVal, Mathf.Abs(dHeelVal), dispDataTable1D, heelDataTable1D, kbtDataTable2D);
            dKMTVal = Interpolate2D(dDispVal, Mathf.Abs(dHeelVal), dispDataTable1D, heelDataTable1D, kmtDataTable2D);


            ClearPointT();//clear point



            float cX = 0;
            float cY = kgDataOrca3D; // ini adalah titik yang tepat, ssw 20140823
            //float cY = dKMTVal;

            // define G point, in Ship coordinate
            float Gx = yCGTotalShip;
            float Gy = zCGTotalShip;

            // define K point, in Ship coordinate, point C as center of rotation, 20140819
            float Kx = 0;
            float Ky = 0;

            // define M point, in Ship coordinate, point C as center of rotation, 20140819
            float Mx = 0;
            float My = dKMTVal;

            // define B point, in Ship coordinate
            float Bx = dTCBVal;
            float By = dKBTVal;

            //// define Z Point, in Ship coordinate
            //float Zx = -dGZVal;
            //float Zy = Gy;

            // define N Point, in Ship coordinate
            float Nx = -dKNVal;
            float Ny = Ky;

            // define G0 point (G lightship) in Ship coordinate, 20150827
            float G0x = yCGLightShip;
            float G0y = zCGLightShip;

            // define Gm point (G total load) in Ship coordinate, 20150827
            Gmx = temp_yCG;//yCGTotalLoad;
            Gmy = temp_zCG;



            // do rotational transformation, point C as center of rotation
            rotate_point(ref Gx, ref Gy, cX, cY, dHeelVal);
            rotate_point(ref Mx, ref My, cX, cY, dHeelVal);
            rotate_point(ref Bx, ref By, cX, cY, dHeelVal);
            rotate_point(ref Nx, ref Ny, cX, cY, dHeelVal);
            rotate_point(ref Kx, ref Ky, cX, cY, dHeelVal);
            rotate_point(ref G0x, ref G0y, cX, cY, dHeelVal); // 20150827
            rotate_point(ref Gmx, ref Gmy, cX, cY, dHeelVal); // 20150827


            xWL[0] = -300.0f;
            yWL[0] = 0.0f;

            xWL[1] = 300.0f;
            yWL[1] = 0.0f;


            //

            shippoints = (pointd[])shippoints_init.Clone();
            for (int i = 0; i < shippoints.Length; i++)
            {
                rotate_point(ref shippoints[i].x, ref shippoints[i].y, cX, cY, dHeelVal);
                pointSeries0.Add(new Vector2(shippoints[i].x, shippoints[i].y));
            }


            //rotate_point(ref Gx, ref Gy, cX, cY, Konv_Sudut(dHeel));
            //rotate_point(ref Kx, ref Ky, cX, cY, Konv_Sudut(dHeel));
            //rotate_point(ref Mx, ref My, cX, cY, Konv_Sudut(dHeel));
            //rotate_point(ref Bx, ref By, cX, cY, Konv_Sudut(dHeel));

            //// correct B, Z and M points (re-rotate M point)
            //// SSW, 20140823
            //Zx = (Zx + Bx) / 2;
            //Bx = Zx;
            //dGZVal = -Zx;
            //dTCBVal = Bx;
            //if (Mathf.Abs(Konv_Sudut(dHeel)) > 0.01f)
            //    dKMVal = kgData + dGZVal / Mathf.Sin(Konv_Sudut(dHeel) * Mathf.PI / 180);
            //Mx = 0;
            //My = dKMVal;
            //rotate_point(ref Mx, ref My, cX, cY, Konv_Sudut(dHeel));

            //// calculate and draw KN
            //float BM = My - By;
            //float KN = BM * Mathf.Tan(Konv_Sudut(dHeel) * Mathf.PI / 180);
            //float Nx = Bx;
            //float Ny = Ky;
            // calculate and draw GZ, calculate KN correction
            // note: if we calculate KN directly from knData,
            // we will get inconsisten position of KN, so we need to correct it
            // ssw, 20150824
            float BMx = Mx - Bx;
            float BMy = My - By;
            float GMy = My - Gy;
            float KMy = My - Ky;
            float Zx = 0;
            dGZVal = 0;
            if (BMx != 0)
            {
                Zx = Mx - GMy / BMy * BMx;
                dGZVal = Mathf.Sign(My - Gy) * Mathf.Abs(Zx - Gx);
                Nx = Mx - KMy / BMy * BMx;
            }
            float Zy = Gy;
            Ny = Ky;
            if (dGZVal < 0)
            {
                // txbGZInfo.ForeColor = Color.Red;
                //txbGZInfo.Text = "Maximum Heel Angle !!!";
            }
            else
            {
                //txbGZInfo.ForeColor = Color.Green;
                //txbGZInfo.Text = "Heel Angle is Allowed";
            }

            //draw water lain
            for (int i = 0; i < 2; i++)
            {
                pointSeries1.Add(new Vector2(xWL[i], dDraftVal));
            }

            //draw KM line
            pointSeries2.Add(new Vector2(Mx, My)); // series 2, M point
            pointSeries2.Add(new Vector2(Kx, Ky)); // series 2, K point

            //draw BM line
            pointSeries3.Add(new Vector2(Mx, My)); // series 3, M point
            pointSeries3.Add(new Vector2(Bx, By)); // series 3, B point

            // draw GZ line
            pointSeries4.Add(new Vector2(Gx, Gy)); // series 4, G point
            pointSeries4.Add(new Vector2(Zx, Zy)); // series 4, Z point

            // draw KN line
            pointSeries5.Add(new Vector2(Kx, Ky)); // series 5, K point
            pointSeries5.Add(new Vector2(Nx, Ny)); // series 5, N point

            // Draw G, B, M, Z point
            pointSeries6.Add(new Vector2(Gx, Gy));
            pointSeries7.Add(new Vector2(Bx, By));
            pointSeries8.Add(new Vector2(Mx, My));  // series 8, M point
            pointSeries9.Add(new Vector2(Zx, Zy));  // series 9, Z point
            pointSeries10.Add(new Vector2(Kx, Ky)); // series 10, K point
            pointSeries11.Add(new Vector2(Nx, Ny)); // series 11, N poi


            pointSeries25.Add(new Vector2(G0x, G0y)); // series 10, K point
            pointSeries26.Add(new Vector2(Gmx, Gmy));

            //txbGZValue.Text = dGZVal.ToString("F2");
            //txbKBValue.Text = dKBVal.ToString("F2");
            //txbKMValue.Text = dKMVal.ToString("F2");
            //txbDraftValue.Text = dOutput1.ToString("F2");
            //txbKGValue.Text = kgData.ToString("F2");
            //txbTCBValue.Text = dTCBVal.ToString("F2");




        }// for sensor

        public void CalculateLongHydrostatic()
        {
            ChangePointLong();
            Rumus();

            float tempDisp;
            float tempTrim;
            float tempDraft;

            float _mTotal = Tray1 + Tray2 + Tray3 + Tray4 + Tray5;


            if (menuInput == true)
            {
                tempDisp = dWeightTotalShip;
                tempTrim = -_trim;
                tempDraft = Interpolate1D(dDispVal, dispDataTable1D, draftDataTable1D);
                _BobotTotal = dWeightTotalLoad;
            }
            else
            {
                fromAI();
               // DraftSim();
                tempDisp = _mTotal + total_Arm + _bobotAwal;
                tempTrim = Konv_Sudut(sldTrim);
                tempDraft = tDraft + 50.0f;
                _BobotTotal = _mTotal + total_Arm;

            }



            dDispVal = tempDisp;
            dDraftVal = tempDraft;
            //dOutput1 = dDraftVal;


            dPitchVal = tempTrim;
            dTrimVal = -dPitchVal;

            dKBLVal = Interpolate2D(dDispVal, dTrimVal, dispDataTable1D, trimDataTable1D, kblDataTable2D);
            dKMLVal = Interpolate2D(dDispVal, dTrimVal, dispDataTable1D, trimDataTable1D, kmlDataTable2D);
            dLCBVal = Interpolate2D(dDispVal, dTrimVal, dispDataTable1D, trimDataTable1D, lcbDataTable2D);

            dLCFVal = Interpolate2D(dDispVal, dTrimVal, dispDataTable1D, trimDataTable1D, lcfDataTable2D);
            dKCFVal = Interpolate2D(dDispVal, dTrimVal, dispDataTable1D, trimDataTable1D, kcfDataTable2D);

            //dLWLVal = Interpolate1D(dDispVal, dispDataTable1D, lwlDataTable1D);
            //dBMLVal = Interpolate1D(dDispVal, dispDataTable1D, bmlDataTable1D);



            ClearPointL();

            // Set reference points: center of rotation
            //double cX = dLCFVal;
            //double cY = dKCFVal; // ini adalah titik yang tepat, ssw 20140823
            float cX = 0;
            float cY = kgDataOrca3D; // ini adalah titik yang tepat, ssw 20140823

            // set G point, in Ship coordinate
            float Gx = xCGTotalShip;
            float Gy = zCGTotalShip;

            // set K point, in Ship coordinate, 
            float Kx = 0;
            float Ky = 0;

            // set M point, in Ship coordinate, 
            float Mx = dLCFVal;
            float My = dKMLVal;

            // set B point, in Ship coordinate
            float Bx = dLCBVal;
            float By = dKBLVal;

            // set COF point, in Ship coordinate
            float COFx = dLCFVal;
            float COFy = dKCFVal;

            xWL[0] = -1000.0f;
            yWL[0] = 0.0f;

            xWL[1] = 1100.0f;
            yWL[1] = 0.0f;

            // set rotable WL point, in Ship coordinate
            float WL1x = 0.9f * xWL[0];
            float WL2x = 0.9f * xWL[1];

            float WL1y = dDraftVal;
            float WL2y = dDraftVal;


            // draw rotated longitudinal plane of the ship
            shippointslon = (pointd[])shippointslon_init.Clone();
            for (int i = 0; i < shippointslon.Length; i++)
            {
                rotate_point(ref shippointslon[i].x, ref shippointslon[i].y, 0, dDraftVal, dPitchVal);
                pointSeries12.Add(new Vector2(shippointslon[i].x, shippointslon[i].y));
            }

            // define G0 point (G lightship) in Ship coordinate, 20150827
            float G0x = xCGLightShip;
            float G0y = zCGLightShip;

            // define Gm point (G total load) in Ship coordinate, 20150827
            float Gmx = xCGTotalLoad;
            float Gmy = zCGTotalLoad;


            rotate_point(ref Gx, ref Gy, cX, cY, dPitchVal);
            rotate_point(ref Kx, ref Ky, cX, cY, dPitchVal);
            rotate_point(ref Mx, ref My, cX, cY, dPitchVal);
            rotate_point(ref Bx, ref By, cX, cY, dPitchVal);
            rotate_point(ref COFx, ref COFy, cX, cY, dPitchVal);
            rotate_point(ref WL1x, ref WL1y, cX, cY, dPitchVal);
            rotate_point(ref WL2x, ref WL2y, cX, cY, dPitchVal);
            rotate_point(ref G0x, ref G0y, cX, cY, dPitchVal);
            rotate_point(ref Gmx, ref Gmy, cX, cY, dPitchVal);

            // draw static WL (waterline)
            for (int i = 0; i < 2; i++)
            {
                pointSeries13.Add(new Vector2(xWL[i], dDraftVal));
            }

            // draw rotated WL line
            pointSeries14.Add(new Vector2(WL1x, WL1y));
            pointSeries14.Add(new Vector2(WL2x, WL2y));

            // draw KML line
            pointSeries15.Add(new Vector2(Mx, My)); // series 3, M point
            pointSeries15.Add(new Vector2(Kx, Ky)); // series 3, K point

            // draw BM line
            pointSeries16.Add(new Vector2(Mx, My)); // series 4, M point
            pointSeries16.Add(new Vector2(Bx, By)); // series 4, B point

            // Draw G, B, M, COF point
            pointSeries17.Add(new Vector2(Gx, Gy));
            pointSeries18.Add(new Vector2(Bx, By));
            pointSeries19.Add(new Vector2(Mx, My));
            pointSeries20.Add(new Vector2(COFx, COFy));

            pointSeries27.Add(new Vector2(G0x, G0y));
            pointSeries28.Add(new Vector2(Gmx, Gmy));

            // Show values in textbox
            //txbKMLValue.Text = dLonKMVal.ToString("F2");
            //txbLCBValue.Text = dLCBVal.ToString("F2");
            //txbLCFValue.Text = dLCFVal.ToString("F2");

        }//for sensor

		private void CalculateStrengthCurve_CO () // 20140926, 20141003, Tanker Ship Model
		{

            ChangePointCurve();

				// Container Ship Model
				// Note SSW, 20141003 : 
				// Container Hull = General Cargo Hull, BUT DIFFER IN cargo compartment

				float sb1 = 0.9380f;
				float sb2 = 0.9728f;
				float sf1 = 0.1900f;
				float sf2 = 0.2180f;
				float sf3 = 0.2690f;
				float sf4 = 0.2000f;
				float sf5 = 0.2900f;
				float lb1 = 0.4690f;
				float lb2 = 0.4864f;
				float lf1 = 0.6730f;
				float lf2 = 0.4270f;
				float lf3 = 0.1345f;
				float lf4 = 0.1450f;
				float lf5 = 0.4403f;

				// load
				//double F1 = 4.00; // kgf
				//double F2 = 3.20; // kgf
				//double F3 = 4.32; // kgf
				//double F4 = 2.72; // kgf
				//double F1 = (double)(scbInputF1.Maximum - scbInputF1.Value) / 1000; // kgf
				//double F2 = (double)(scbInputF2.Maximum - scbInputF2.Value) / 1000; // kgf
				//double F3 = (double)(scbInputF3.Maximum - scbInputF3.Value) / 1000; // kgf
				//double F4 = (double)(scbInputF4.Maximum - scbInputF4.Value) / 1000; // kgf

                if (menuInput == true)
                {
                    H1 = iBay1Row0 + iBay1Row1 + iBay1Row2 + iBay1Row3 + iBay1Row4 +
                        iBay3Row0 + iBay3Row1 + iBay3Row2 + iBay3Row3 + iBay3Row4;

                    H2 = iBay5Row0 + iBay5Row1 + iBay5Row2 + iBay5Row3 + iBay5Row4 +
                        iBay7Row0 + iBay7Row1 + iBay7Row2 + iBay7Row3 + iBay7Row4;

                    H3 = iBay9Row0 + iBay9Row1 + iBay9Row2 + iBay9Row3 + iBay9Row4 +
                        iBay11Row0 + iBay11Row1 + iBay11Row2 + iBay11Row3 + iBay11Row4;

                    H4 = iBay13Row0 + iBay13Row1 + iBay13Row2 + iBay13Row3 + iBay13Row4 +
                        iBay15Row0 + iBay15Row1 + iBay15Row2 + iBay15Row3 + iBay15Row4;

                    H5 = iBay17Row0 + iBay17Row1 + iBay17Row2 + iBay17Row3 + iBay17Row4 +
                        iBay19Row0 + iBay19Row1 + iBay19Row2 + iBay19Row3 + iBay19Row4;
                }
                else
                {

                    H1 = Hold1_1FCTR + Hold1_1S + Hold1_2P + Hold1_2S;
                    H2 = Hold2_1P + Hold2_1S + Hold2_2P + Hold2_2S;
                    H3 = Hold3_1P + Hold3_1S + Hold3_2P + Hold3_2S;
                    H4 = Hold4_1P + Hold4_1S + Hold4_2P + Hold4_2S;
                    H5 = Hold5_1P + Hold5_1S + Hold5_2BCTR + Hold5_2S;
                }
              

				float Ftot = H1 + H2 + H3 + H4 + H5;
				float B1 = (Ftot * lb2 + (H1 * lf1 + H2 * lf2 + H3 * lf3 - H4 * lf4 - H5 * lf5)) / (lb1 + lb2);
				float B2 = (Ftot * lb1 - (H1 * lf1 + H2 * lf2 + H3 * lf3 - H4 * lf4 - H5 * lf5)) / (lb1 + lb2);
				
				float sigmaB1 = B1 / sb1;
				float sigmaB2 = B2 / sb2;
				float sigmaF1 = H1 / sf1;
				float sigmaF2 = H2 / sf2;
				float sigmaF3 = H3 / sf3;
				float sigmaF4 = H4 / sf4;
				float sigmaF5 = H5 / sf5;

				// Longitudinal data in chart
				// series[0]  = hull ship, longitudinal
				// series[1]  = load line 
				// series[2]  = shear force line 
				// series[3]  = bending moment line 

				// clear all series, except series number 0 (hull ship form)
				//for (int i = 1; i < crtStrengthCurve.Series.Count; i++)
				//{
				//    crtStrengthCurve.Series[i].Points.Clear();
				//}
				
				ClearPointS ();

				shippointslon = (pointd[])shippointslon_init.Clone ();
				for (int i = 0; i < shippointslon.Length; i++) {
						//rotate_point(ref shippointslon[i].x, ref shippointslon[i].y, 0, dDraftVal, Konv_Sudut(sldTrim));
						pointSeries24.Add (new Vector2 (shippointslon [i].x, shippointslon [i].y));
				}





				// calculate load
				//double x;
				//double dx;
				//double sigma;
				//double shearforce = 0;
				//double bdgmoment = 0;
				float x;
				float dx;
				float sigma;
				float shearforce = 0;
				float bdgmoment = 0;
				int iRef = 0;

				for (int i = 0; i < lonpos_BC.Length; i++) { // Bulk Carrier
						if (i < 3) {
								iRef = 0;
								sigma = sigmaB2;
						} else if (i < 6) { // compartment 5
								iRef = 2;
								sigma = sigmaB2 - sigmaF5;
						} else if (i < 9) {
								iRef = 5;
								sigma = sigmaB2;
						} else if (i < 12) { // compartment 4
								iRef = 8;
								sigma = sigmaB2 - sigmaF4;
						} else if (i < 15) {
								iRef = 11;
								sigma = sigmaB2;
						} else if (i < 18) { // compartment 3
								iRef = 14;
								sigma = sigmaB1 - sigmaF3;
						} else if (i < 21) {
								iRef = 17;
								sigma = sigmaB1;
						} else if (i < 24) { // compartment 2
								iRef = 20;
								sigma = sigmaB1 - sigmaF2;
						} else if (i < 27) {
								iRef = 23;
								sigma = sigmaB1;
						} else if (i < 30) { // compartment 1
								iRef = 26;
								sigma = sigmaB1 - sigmaF1;
						} else {
								iRef = 29;
								sigma = sigmaB1;
						}

						dx = (lonpos_BC [i].x - lonpos_BC [iRef].x) * 0.001f; // in meter
						if (i > 0) {
								//shearforce = shearline_series.Points[iRef].YValues[0] / scale_sf + sigma * dx;
								shearforce = pointSeries22 [iRef].y / scale_sf + sigma * dx;
								//bdgmoment = bmomentline_series.Points[iRef].YValues[0] / scale_bm + 0.5f * (shearline_series.Points[iRef].YValues[0] / scale_sf + shearforce) * dx;
								bdgmoment = pointSeries23 [iRef].y / scale_bm + 0.5f * (pointSeries22 [iRef].y / scale_sf + shearforce) * dx;                            
						}

						x = lonpos_BC [i].x - LCG_BC; // in mm

						pointSeries21.Add (new Vector2 (x, sigma * scale_ld));//load
						pointSeries22.Add (new Vector2 (x, shearforce * scale_sf));//shearline_series
						pointSeries23.Add (new Vector2 (x, bdgmoment * scale_bm));//bmomentLine_series

						//print (" data ke-" + i + " " + bdgmoment * scale_bm);


				}



				//// searching for zero shear force
				//float sfa, sfb, xa, xb;
				//WMG_Series zerosf_pos = new WMG_Series();
				//WMG_Series ld_zerosf = new WMG_Series(); // load distribution (sigma) at zero sf
				//WMG_Series bm_zerosf = new WMG_Series(); // bending moment at zero sf
				//int k = 0;
				//while (k < shearline_series.pointValues.Count - 1)
				//{
				//    //sfa = shearline_series.pointValues[k].y;
				//    sfa = pointSeries22[k].y;
				//    //sfb = shearline_series.pointValues[k + 1].y;
				//    sfb = pointSeries22[k + 1].y;

				//    if (sfa * sfb < 0) // curve crossing x-axis, so there must be zero shear force
				//    {
				//        //xa = shearline_series.pointValues[k].x; // in mm
				//        xa = pointSeries22[k].x;
				//        xb = pointSeries22[k + 1].x; // in mm
				//        x = (xa - sfa / sfb * xb) / (1 - sfa / sfb);
				//        dx = (x - xa) * 0.001f; // in meter
				//        //sigma = (loadline_series.Points[i].YValues[0] + loadline_series.Points[i + 1].YValues[0]) / 2;
				//        sigma = pointSeries21[k].y / scale_ld;
				//        shearforce = 0;
				//        bdgmoment = 0;// bmomentLine_series.pointValues[k] / scale_bm + 0.5f * (shearline_series.pointValues[k].y / scale_sf) * dx;
				//        //zerosf_pos.Points.AddXY(i, x); //
				//        //ld_zerosf.Points.AddXY(i, sigma);
				//        //bm_zerosf.Points.AddXY(i, bdgmoment);
				//        pointSeries21.Insert(new Vector2(k + 1, x, sigma * scale_ld));
				//        pointSeries22.Insert(new Vector2(k + 1, x, shearforce * scale_sf));
				//        pointSeries23.Insert(new Vector2(k + 1, x, bdgmoment * scale_bm));



				//        //pointSeries21.Add(new Vector2(x, sigma * scale_ld));//load
				//        //pointSeries22.Add(new Vector2(x, shearforce * scale_sf));//shearline_series
				//        //pointSeries23.Add(new Vector2(x, bdgmoment * scale_bm));//bmomentLine_series
				//    }
				//    k += 1;
				//} // end of while

				// show in listview
				//lsvStrengthCurve.Items.Clear();
				//ListViewItem lvi = null;
				//for (int i = 0; i < loadline_series.Points.Count; i++)
				//{
				//    sigma = loadline_series.Points[i].YValues[0] / scale_ld;
				//    shearforce = shearline_series.Points[i].YValues[0] / scale_sf;
				//    bdgmoment = bmomentline_series.Points[i].YValues[0] / scale_bm;
				//    lvi = new ListViewItem(loadline_series.Points[i].XValue.ToString("F2"));
				//    lvi.SubItems.Add(sigma.ToString("F3")); // load
				//    lvi.SubItems.Add(shearforce.ToString("F3")); // shear force
				//    lvi.SubItems.Add(bdgmoment.ToString("F3")); // bending moment
				//    lsvStrengthCurve.Items.Add(lvi);
				//}

		}

		void InitShipDataS ()
		{
            knDataTable2D = (float[,])knData_BC.Clone();
            tcbDataTable2D = (float[,])tcbData_BC.Clone();
            kbtDataTable2D = (float[,])kbtData_BC.Clone();
            kmtDataTable2D = (float[,])kmtData_BC.Clone();
            lcbDataTable2D = (float[,])lcbData_BC.Clone();
            kblDataTable2D = (float[,])kblData_BC.Clone();
            kmlDataTable2D = (float[,])kmlData_BC.Clone();
            lcfDataTable2D = (float[,])lcfData_BC.Clone();
            kcfDataTable2D = (float[,])kcfData_BC.Clone();
            draftDataTable1D = (float[])draftData_BC.Clone();
            heelDataTable1D = (float[])heelData_BC.Clone();
            dispDataTable1D = (float[])dispData_BC.Clone();
            trimDataTable1D = (float[])trimData_BC.Clone();

            kgDataOrca3D = KG_BC_ORCA3D;
            kgDataReal = KG_BC_REAL;

            // initialize amidship points, then translate each point 
            shippoints_init = (pointd[])shippoints_BC.Clone();
            for (int i = 0; i < shippoints_init.Length; i++)
            {
                shippoints_init[i].y += 102.443f;
            }

            // initialize ship longitudinal plane 
            shippointslon_init = (pointd[])shippointslon_BC.Clone();


		}

        void ChangePointHydrostatic()//calculate hydrostatic
        {


            //GameObject.Find("s0-Ship").GetComponent("WMG_Series").SendMessage("setPointValues", pointSeries0);
            //GameObject.Find("s0-Ship").GetComponent("WMG_Series").SendMessage("setPointValuesChanged", true);

            wPoint0.setPointValues(pointSeries0);
            wPoint0.setPointValuesChanged(true);

            wPoint1.setPointValues(pointSeries1);
            wPoint1.setPointValuesChanged(true);

            wPoint2.setPointValues(pointSeries2);
            wPoint2.setPointValuesChanged(true);

            wPoint3.setPointValues(pointSeries3);
            wPoint3.setPointValuesChanged(true);

            wPoint4.setPointValues(pointSeries4);
            wPoint4.setPointValuesChanged(true);

            wPoint5.setPointValues(pointSeries5);
            wPoint5.setPointValuesChanged(true);

            wPoint6.setPointValues(pointSeries6);
            wPoint6.setPointValuesChanged(true);

            wPoint7.setPointValues(pointSeries7);
            wPoint7.setPointValuesChanged(true);

            wPoint8.setPointValues(pointSeries8);
            wPoint8.setPointValuesChanged(true);

            wPoint9.setPointValues(pointSeries9);
            wPoint9.setPointValuesChanged(true);

            wPoint10.setPointValues(pointSeries10);
            wPoint10.setPointValuesChanged(true);

            wPoint11.setPointValues(pointSeries11);
            wPoint11.setPointValuesChanged(true);

			wPoint25.setPointValues(pointSeries25);
			wPoint25.setPointValuesChanged(true);


            wPoint26.setPointValues(pointSeries26);
            wPoint26.setPointValuesChanged(true);

          




        }

		void ChangePointCurve ()//calculate strength curve
		{
            //ClearPointL();
            //ClearPointT();

            //wPoint21.setPointValues(pointSeries21);
            //wPoint21.setPointValuesChanged(true);

            //wPoint22.setPointValues(pointSeries22);
            //wPoint22.setPointValuesChanged(true);

            //wPoint23.setPointValues(pointSeries23);
            //wPoint23.setPointValuesChanged(true);

            //wPoint24.setPointValues(pointSeries24);
            //wPoint24.setPointValuesChanged(true);

		}

		void ChangePointLong ()
		{


            wPoint12.setPointValues(pointSeries12);
            wPoint12.setPointValuesChanged(true);

            wPoint13.setPointValues(pointSeries13);
            wPoint13.setPointValuesChanged(true);

            wPoint14.setPointValues(pointSeries14);
            wPoint14.setPointValuesChanged(true);

            wPoint15.setPointValues(pointSeries15);
            wPoint15.setPointValuesChanged(true);

            wPoint16.setPointValues(pointSeries16);
            wPoint16.setPointValuesChanged(true);

            wPoint17.setPointValues(pointSeries17);
            wPoint17.setPointValuesChanged(true);

            wPoint18.setPointValues(pointSeries18);
            wPoint18.setPointValuesChanged(true);

            wPoint19.setPointValues(pointSeries19);
            wPoint19.setPointValuesChanged(true);

            wPoint20.setPointValues(pointSeries20);
            wPoint20.setPointValuesChanged(true);

            wPoint27.setPointValues(pointSeries27);
            wPoint27.setPointValuesChanged(true);

            wPoint28.setPointValues(pointSeries28);
            wPoint28.setPointValuesChanged(true);



		}

    public void ObjectPayload()
    {
        menuInput = !menuInput;
    }
		void OnGUI ()
		{



        //    #region Menu
        //    if (GUI.Button(new Rect(420 + posX, 50, btWidth, btnHeight), "Comm"))
        //    {
        //        // chart2d.SetActive(false);
        //        port.active = !port.active;

        //    }
        //    if (GUI.Button(new Rect(520 + posX, 50, btWidth, btnHeight), "View Mode"))
        //    {
        //        menuMode = !menuMode;// true;
        //    }
        //    if (menuMode)
        //    {
        //        frmMode = GUI.Window(1, frmMode, formMode, "Mode/FX");
        //        menuMode = true;
        //    }

        //    //if (GUI.Button(new Rect(660 + posX, 200, btWidth, btnHeight), "Simulation"))
        //    //{
        //    //    //camChart.SetActive(false);
        //    //    // 
        //    //    menuSim = !menuSim;
        //    //}
        //    //if (menuSim)
        //    //{
        //    //    frmSimulasi = GUI.Window(4, frmSimulasi, formSimulasi, "Simulation");
        //    //    menuSim = true;
        //    //    print("aktif");
        //    //}

        //    if (GUI.Button(new Rect(620 + posX, 50, btWidth, btnHeight), "Monitoring"))
        //    {
        //        //// true;
        //        menuCalib = !menuCalib;// true;
        //        print("Calibration");
        //    }
        //    if (menuCalib)
        //    {
        //        frmCalib = GUI.Window(3, frmCalib, formCallib, "Monitoring");
        //        menuCalib = true;
        //    }

        //    if (GUI.Button(new Rect(720 + posX, 50, btWidth, btnHeight), "Curve"))
        //    {
        //        camChart.active = !camChart.active;
        //    }








        //if (GUI.Button(new Rect(820 + posX, 50, btWidth, btnHeight), "Stability Points"))
        //    {

        //        //menuInput = !menuInput;// true;
        //        cam2D.active = !cam2D.active;
        //        print("Input");
        //    }


        //    if (GUI.Button(new Rect(920 + posX, 50, btWidth, btnHeight), "Input Data"))
        //    {
        //        menuInput = !menuInput;
        //    }
        if (menuInput)
        {
            frmInput = GUI.Window(2, frmInput, formInput, "Object Payload");
            menuInput = true;
        }

        //    if (GUI.Button(new Rect(1020 + posX, 50, btWidth, btnHeight), "Help"))
        //    {
        //        //Application.Quit ();
        //        menuHelp = !menuHelp;
        //        print("Help");
        //    }
        //    if (menuHelp)
        //    {
        //        frmHelp = GUI.Window(5, frmHelp, formHelp, "Help");
        //        menuHelp = true;
        //    }


        //    //// GUI FOR BALLAST MODE AUTO MANUAL ===============================================
        //    //if (GUI.Button(new Rect(500 + posX, 50, btWidth, btnHeight), "Ballast MD"))
        //    //{
        //    //    menuBallastMode = !menuBallastMode;
        //    //    //print("Ballast Mode");
        //    //}
        //    //if (menuBallastMode)
        //    //{
        //    //    GUI.Box(new Rect(500 + posX, 85, btWidth, btnHeight), "");
        //    //    GUI.Box(new Rect(500 + posX, 130, 500, 200), "Ballast Mode");
        //    //    GUI.Label(new Rect(510 + posX, 160, 100, 30), "Ballast Mode :");

        //    //    if (GUI.Button(Box, BallastselectedItem))
        //    //    {
        //    //        editing = true;
        //    //    }
        //    //    if (editing)
        //    //    {
        //    //        for (int x = 0; x < BallastModeItems.Length; x++)
        //    //        {
        //    //            if (GUI.Button(new Rect(Box.x, (Box.height * x) + Box.y + Box.height, Box.width, Box.height), BallastModeItems[x]))
        //    //            {
        //    //                if (data != null)
        //    //                {
        //    //                    if (data.SerialPort != null)
        //    //                    {
        //    //                        if (data.SerialPort.IsOpen)
        //    //                        {
        //    //                            BallastselectedItem = BallastModeItems[x];
        //    //                            BallastMode = x;
        //    //                        }
        //    //                    }
        //    //                }
        //    //                editing = false;
        //    //            }
        //    //        }
        //    //    }


        //    //}
        //    //else
        //    //{
        //    //    //print("EXIT MENU BALLAST MODE");
        //    //}
        //    //// GUI FOR BALLAST MODE AUTO MANUAL ===============================================

        //    if (GUI.Button(new Rect(1120 + posX, 50, btWidth, btnHeight), "Exit"))
        //    {
        //        menuExit = !menuExit;// true;
        //        print("Exit");
        //    }
        //    if (menuExit)
        //    {

        //        frmExit = GUI.Window(5, frmExit, formExit, "Exit Application ?");
        //        menuExit = true;

        //    }
        //    #endregion


    }//menu utama

        void fromManualInput()
        {

            //RollPitch();
            //TrimList();
            //DraftSim();

            //ballastIsi();
            //waterDB();

            //CalculateStrengthCurve_CO();
            //CalculateTranverseHydrostatic();
            //CalculateLongHydrostatic();
            
        }

        void fromAI()
        {

           
            TrimList();
           

            ballastIsi();
            waterDB();


        //CargoHold();
        cargoIsi();

           
          
            //SaveData();
        }

        public void Rumus()
        {

            //_m1 = Tray1;// Vol_Cargo1(H1);
            //_m2 = Tray2;//Vol_Cargo2(H2);
            //_m3 = Tray3;//Vol_Cargo3(H3);
            //_m4 = Tray4;//Vol_Cargo4(H4);
            //_m5 = Tray5;//Vol_Cargo5(H5);


            ////_m1 = Vol_Cargo1(H1);
            ////_m2 = Vol_Cargo2(H2);
            ////_m3 = Vol_Cargo3(H3);
            ////_m4 = Vol_Cargo4(H4);
            ////_m5 = Vol_Cargo5(H5);

            ////_Gz1 = 47 + level_Cargo1(H1) / 2;
            ////_Gz2 = 47 + level_Cargo2(H2) / 2;
            ////_Gz3 = 47 + level_Cargo3(H3) / 2;
            ////_Gz4 = 47 + level_Cargo4(H4) / 2;
            ////_Gz5 = 47 + level_Cargo5(H5) / 2;
           
            //_Gz1 = 47 + _m1 / 2;
            //_Gz2 = 47 + _m2 / 2;
            //_Gz3 = 47 + _m3 / 2;
            //_Gz4 = 47 + _m4 / 2;
            //_Gz5 = 47 + _m5 / 2;


            ////jumlah total muatan
            //_mTotal = _m1 + _m2 + _m3 + _m4 + _m5;
            ////---------------------
            ////Pusat massa total muatan
            //_Gmx = (_m1 * _Gx1 + _m2 * _Gx2 + _m3 * _Gx3 + _m4 * _Gx4 + _m5 * _Gx5) / _mTotal;

            //_Gmy = (_m1 * _Gy1 + _m2 * _Gy2 + _m3 * _Gy3 + _m4 * _Gy4 + _m5 * _Gy5) / _mTotal;

            //_Gmz = (_m1 * _Gz1 + _m2 * _Gz2 + _m3 * _Gz3 + _m4 * _Gz4 + _m5 * _Gz5) / _mTotal;
                  

            ////Pusat massa total kapal
            ////--------------------------------------------------
            //_Gx1M = (_bobotAwal * _Gx0 + _mTotal * _Gmx) / (_bobotAwal + _mTotal);
            //_Gy1M = (_bobotAwal * _Gy0 + _mTotal * _Gmy) / (_bobotAwal + _mTotal);
            //_Gz1M = (_bobotAwal * _Gz0 + _mTotal * _Gmz) / (_bobotAwal + _mTotal);

            total_Arm = TuasKananDepan(AF1SB) + TuasKananBelakang(AF2SB) + TuasKiriDepan(AF1P) + TuasKiriBelakang(AF2P);


        }

        void formInput(int windowID)
        {


            GUI.Label(new Rect(35, 30, 100, 30), "Cargo 5");
            GUI.Label(new Rect(125, 30, 100, 30), "Cargo 4");
            GUI.Label(new Rect(215, 30, 100, 30), "Cargo 3");
            GUI.Label(new Rect(305, 30, 100, 30), "Cargo 2");
            GUI.Label(new Rect(395, 30, 100, 30), "Cargo 1");


            //TRAY 5
            if (GUI.Button(new Rect(20, 60, 40, 30), iBay19Row1.ToString()))
            {
                iBay19Row1 += 1;
                if (iBay19Row1 > 5) iBay19Row1 = 0;
                cargoList[29].transform.localScale = new Vector3(1, (float)iBay19Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(20, 90, 40, 30),iBay19Row0.ToString() ))
            {
                iBay19Row0 += 1;
                if (iBay19Row0 > 5) iBay19Row0 = 0;
                cargoList[28].transform.localScale = new Vector3(1, (float)iBay19Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(20, 120, 40, 30), iBay19Row2.ToString()))
            {
                iBay19Row2 += 1;
                if (iBay19Row2 > 5) iBay19Row2 = 0;
                cargoList[26].transform.localScale = new Vector3(1, (float)iBay19Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            if (GUI.Button(new Rect(60, 60, 40, 30), iBay17Row1.ToString()))
            {
                iBay17Row1 += 1;
                if (iBay17Row1 > 5) iBay17Row1 = 0;
                cargoList[27].transform.localScale = new Vector3(1, (float)iBay17Row1 / 5, 1);
               //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(60, 90, 40, 30), iBay17Row0.ToString()))
            {
                iBay17Row0 += 1;
                if (iBay17Row0 > 5) iBay17Row0 = 0;
                cargoList[25].transform.localScale = new Vector3(1, (float)iBay17Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(60, 120, 40, 30), iBay17Row2.ToString()))
            {
                iBay17Row2 += 1;
                if (iBay17Row2 > 5) iBay17Row2 = 0;
                cargoList[24].transform.localScale = new Vector3(1, (float)iBay17Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            //TRAY 4
            if (GUI.Button(new Rect(110, 60, 40, 30), iBay15Row1.ToString()))
            {
                iBay15Row1 += 1;
                if (iBay15Row1 > 5) iBay15Row1 = 0;
                cargoList[23].transform.localScale = new Vector3(1, (float)iBay15Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(110, 90, 40, 30), iBay15Row0.ToString()))
            {
                iBay15Row0 += 1;
                if (iBay15Row0 > 5) iBay15Row0 = 0;
                cargoList[22].transform.localScale = new Vector3(1, (float)iBay15Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(110, 120, 40, 30), iBay15Row2.ToString()))
            {
                iBay15Row2 += 1;
                if (iBay15Row2 > 5) iBay15Row2 = 0;
                cargoList[20].transform.localScale = new Vector3(1, (float)iBay15Row2 / 5, 1);
               //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            if (GUI.Button(new Rect(150, 60, 40, 30), iBay13Row1.ToString()))
            {
                iBay13Row1 += 1;
                if (iBay13Row1 > 5) iBay13Row1 = 0;
                cargoList[21].transform.localScale = new Vector3(1, (float)iBay13Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(150, 90, 40, 30), iBay13Row0.ToString()))
            {
                iBay13Row0 += 1;
                if (iBay13Row0 > 5) iBay13Row0 = 0;
                cargoList[19].transform.localScale = new Vector3(1, (float)iBay13Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(150, 120, 40, 30), iBay13Row2.ToString()))
            {
                iBay13Row2 += 1;
                if (iBay13Row2 > 5) iBay13Row2 = 0;
                cargoList[18].transform.localScale = new Vector3(1, (float)iBay13Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            //TRAY 3
            if (GUI.Button(new Rect(200, 60, 40, 30), iBay11Row1.ToString()))
            {
                iBay11Row1 += 1;
                if (iBay11Row1 > 5) iBay11Row1 = 0;
                cargoList[17].transform.localScale = new Vector3(1, (float)iBay11Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(200, 90, 40, 30), iBay11Row0.ToString()))
            {
                iBay11Row0 += 1;
                if (iBay11Row0 > 5) iBay11Row0 = 0;
                cargoList[16].transform.localScale = new Vector3(1, (float)iBay11Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(200, 120, 40, 30), iBay11Row2.ToString()))
            {
                iBay11Row2 += 1;
                if (iBay11Row2 > 5) iBay11Row2 = 0;
                cargoList[14].transform.localScale = new Vector3(1, (float)iBay11Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            if (GUI.Button(new Rect(240, 60, 40, 30), iBay9Row1.ToString()))
            {
                iBay9Row1 += 1;
                if (iBay9Row1 > 5) iBay9Row1 = 0;
                cargoList[15].transform.localScale = new Vector3(1, (float)iBay9Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(240, 90, 40, 30), iBay9Row0.ToString()))
            {
                iBay9Row0 += 1;
                if (iBay9Row0 > 5) iBay9Row0 = 0;
                cargoList[13].transform.localScale = new Vector3(1, (float)iBay9Row0 / 5, 1);
               //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(240, 120, 40, 30), iBay9Row2.ToString()))
            {
                iBay9Row2 += 1;
                if (iBay9Row2 > 5) iBay9Row2 = 0;
                cargoList[12].transform.localScale = new Vector3(1, (float)iBay9Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            //TRAY 2
            if (GUI.Button(new Rect(290, 60, 40, 30), iBay7Row1.ToString()))
            {
                iBay7Row1 += 1;
                if (iBay7Row1 > 5) iBay7Row1 = 0;
                cargoList[11].transform.localScale = new Vector3(1, (float)iBay7Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(290, 90, 40, 30), iBay7Row0.ToString()))
            {
                iBay7Row0 += 1;
                if (iBay7Row0 > 5) iBay7Row0 = 0;
                cargoList[10].transform.localScale = new Vector3(1, (float)iBay7Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(290, 120, 40, 30), iBay7Row2.ToString()))
            {
                iBay7Row2 += 1;
                if (iBay7Row2 > 5) iBay7Row2 = 0;
                cargoList[8].transform.localScale = new Vector3(1, (float)iBay7Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            if (GUI.Button(new Rect(330, 60, 40, 30), iBay5Row1.ToString()))
            {
                iBay5Row1 += 1;
                if (iBay5Row1 > 5) iBay5Row1 = 0;
                cargoList[9].transform.localScale = new Vector3(1, (float)iBay5Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(330, 90, 40, 30), iBay5Row0.ToString()))
            {
                iBay5Row0 += 1;
                if (iBay5Row0 > 5) iBay5Row0 = 0;
                cargoList[7].transform.localScale = new Vector3(1, (float)iBay5Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(330, 120, 40, 30), iBay5Row2.ToString()))
            {
                iBay5Row2 += 1;
                if (iBay5Row2 > 5) iBay5Row2 = 0;
                cargoList[6].transform.localScale = new Vector3(1, (float)iBay5Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            //TRAY 1
            if (GUI.Button(new Rect(380, 60, 40, 30), iBay3Row1.ToString()))
            {
                iBay3Row1 += 1;
                if (iBay3Row1 > 5) iBay3Row1 = 0;
                cargoList[5].transform.localScale = new Vector3(1, (float)iBay3Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(380, 90, 40, 30), iBay3Row0.ToString()))
            {
                iBay3Row0 += 1;
                if (iBay3Row0 > 5) iBay3Row0 = 0;
                cargoList[4].transform.localScale = new Vector3(1, (float)iBay3Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(380, 120, 40, 30), iBay3Row2.ToString()))
            {
                iBay3Row2 += 1;
                if (iBay3Row2 > 5) iBay3Row2 = 0;
                cargoList[2].transform.localScale = new Vector3(1, (float)iBay3Row2 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            //--------------------------------------------
            if (GUI.Button(new Rect(420, 60, 40, 30), iBay1Row1.ToString()))
            {
                iBay1Row1 += 1;   
                if (iBay1Row1 > 5) iBay1Row1 = 0;
                cargoList[3].transform.localScale = new Vector3(1, (float)iBay1Row1 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(420, 90, 40, 30), iBay1Row0.ToString()))
            {
                iBay1Row0 += 1;
                if (iBay1Row0 > 5) iBay1Row0 = 0;
                cargoList[1].transform.localScale = new Vector3(1, (float)iBay1Row0 / 5, 1);
                //CalculateCG_and_Attitude();
            }
            if (GUI.Button(new Rect(420, 120, 40, 30), iBay1Row2.ToString()))
            {
                iBay1Row2 += 1;
              
                if (iBay1Row2 > 5) iBay1Row2 = 0;
                cargoList[0].transform.localScale = new Vector3(1, (float)iBay1Row2 / 5, 1);
                //CalculateCG_and_Attitude();
               
            }
            //--------------------------------------------

            //waterTankT();
            //GUI.DragWindow();



        }

    

      

        void formHelp(int windowID)
        {

            GUI.Box(new Rect(100, 30, 150, 25), "Camera Controller");

            GUI.Box(new Rect(25, 70, 25, 25), "W");

            GUI.Label(new Rect(60, 70, 100, 25), ": Up");

            GUI.Box(new Rect(25, 100, 25, 25), "A");

            GUI.Label(new Rect(60, 100, 100, 25), ": Left");

            GUI.Box(new Rect(25, 130, 25, 25), "S");

            GUI.Label(new Rect(60, 130, 100, 25), ": Down");

            GUI.Box(new Rect(25, 160, 25, 25), "D");

            GUI.Label(new Rect(60, 160, 100, 25), ": Right");

            //---------------------------------------------------------
            GUI.Box(new Rect(150, 70, 100, 25), "Key.Up");

            GUI.Label(new Rect(275, 70, 100, 25), ": Up");

            GUI.Box(new Rect(150, 100, 100, 25), "Key.Left");

            GUI.Label(new Rect(275, 100, 100, 25), ": Left");

            GUI.Box(new Rect(150, 130, 100, 25), "Key.Down");

            GUI.Label(new Rect(275, 130, 100, 25), ": Down");

            GUI.Box(new Rect(150, 160, 100, 25), "Key.Right");

            GUI.Label(new Rect(275, 160, 100, 25), ": Right");

            GUI.DragWindow();



        }

		void TrimList ()
		{
				TrimVal = ((D1P + D1S) / 2) - ((D2P + D2S) / 2);
				ListVal = ((D1P + D2P) / 2) - ((D1S + D2S) / 2);
		}// trim list

		private float levelAI (float ai)//rumus untuk menentukan level sensor
		{

				if (ai >= 2.2f) {
						level = -2.7273f * ai + 11.5f;
				} else if (ai >= 1.55f) {
						level = -3.84615f * ai + 13.96f;
				} else if (ai > 1) {
						level = -8.7273f * ai + 21.523f;
				} else if (ai > 0.8f) {
						level = -16f * ai + 28.8f;
				} else if (ai >= 0.65f) {
						level = 0.7017f * ai + 15.438f;
				} else {
						level = 19;
				}

				float tTangki = 1 - (level - 5) / 14;//tinggi isi tangki
				return tTangki;

		}

		void CargoHold ()
		{

            Tray1 = -0.0016f * H1 * H1 + 0.9865f * H1 - 0.2514f;

            Tray2 = -0.0052f * H2 * H2 + 1.0329f * H2 - 0.5901f;

            Tray3 = -0.0014f * H3 * H3 + 0.9955f * H3 - 0.7226f;

            Tray4 = 0.0011f * H4 * H4 + 0.9898f * H4 - 1.0522f;

            Tray5 = 0.002f * H5 * H5 + 0.9871f * H5 - 0.8573f;


            if (Tray1 < 0)
            {
                Tray1 = 0;

            }
            if (Tray2 < 0)
            {
                Tray2 = 0;

            }
            if (Tray3 < 0)
            {
                Tray3 = 0;

            }
            if (Tray4 < 0)
            {
                Tray4 = 0;

            }
            if (Tray5 < 0)
            {
                Tray5 = 0;

            }

		}

		private float DraftR (float ai)//rumus untuk menentukan level sensor
		{


				if (ai >= 6.5) {
						lvDraft = -15.6f;
				} else if (ai >= 6) {
						lvDraft = -13.5f;
				} else if (ai >= 5.5) {
						lvDraft = -12.5f;
				} else if (ai >= 5) {
						lvDraft = -11.7f;
				} else if (ai >= 4.5) {
						lvDraft = -9.8f;
				} else if (ai >= 4) {
						lvDraft = -9.4f;
				} else if (ai >= 3.5) {
						lvDraft = -7.8f;
				} else if (ai >= 3) {
						lvDraft = -6.8f;
				} else if (ai >= 2.5) {
						lvDraft = -5.5f;
				} else if (ai >= 2) {
						lvDraft = -4.4f;
				} else if (ai >= 1.5) {
						lvDraft = -3.5f;
				} else if (ai >= 1) {
						lvDraft = -2.6f;
				} else if (ai >= 0.5) {
						lvDraft = -1.09f;
				} else if (ai >= 0) {
						lvDraft = 0.0f;
				}
            

				return lvDraft;

		}

		void DataAI ()//cargo tank AI
		{

            //SIDE TANK AI

           
                ST_1P = float.Parse(data.AIDataA[0]);
                ST_2P = float.Parse(data.AIDataA[1]);
                ST_3P = float.Parse(data.AIDataA[2]);
                ST_4P = float.Parse(data.AIDataA[3]);
                ST_5P = float.Parse(data.AIDataA[4]);

                ST_1SB = float.Parse(data.AIDataA[5]);
                ST_2SB = float.Parse(data.AIDataA[6]);
                ST_3SB = float.Parse(data.AIDataA[7]);

                ST_4SB = float.Parse(data.AIDataB[0]);
                ST_5SB = float.Parse(data.AIDataB[1]);

                VWBF = float.Parse(data.AIDataB[2]);
                VAWBP = float.Parse(data.AIDataB[3]);
                VAWBSB = float.Parse(data.AIDataB[4]);
                VAWBLO = float.Parse(data.AIDataB[5]);

                DB_1P = float.Parse(data.AIDataB[6]);
                DB_2P = float.Parse(data.AIDataB[7]);

                DB_3P = float.Parse(data.AIDataC[0]);
                DB_4P = float.Parse(data.AIDataC[1]);
                DB_5P = float.Parse(data.AIDataC[2]);

                DB_1SB = float.Parse(data.AIDataC[3]);
                DB_2SB = float.Parse(data.AIDataC[4]);

                DB_3SB = float.Parse(data.AIDataC[5]);
                DB_4SB = float.Parse(data.AIDataC[6]);
                DB_5SB = float.Parse(data.AIDataC[7]);

                //CARGO hold AI
                Hold1_1FCTR = float.Parse(data.AIDataD[0]);
                Hold1_2S = float.Parse(data.AIDataD[1]);
                Hold1_2P = float.Parse(data.AIDataD[2]);

                Hold2_1P = float.Parse(data.AIDataD[3]);//
                Hold2_1S = float.Parse(data.AIDataD[4]);
                Hold2_2P = float.Parse(data.AIDataD[5]);
                Hold2_2S = float.Parse(data.AIDataD[6]);
                Hold3_1P = float.Parse(data.AIDataD[7]);//

                Hold3_1S = float.Parse(data.AIDataE[0]);
                Hold3_2P = float.Parse(data.AIDataE[1]);
                Hold3_2S = float.Parse(data.AIDataE[2]);
                Hold4_1P = float.Parse(data.AIDataE[3]);

                Hold4_1S = float.Parse(data.AIDataE[4]);
                Hold4_2P = float.Parse(data.AIDataE[5]);
                Hold4_2S = float.Parse(data.AIDataE[6]);

                Hold5_1P = float.Parse(data.AIDataE[7]);

                Hold5_1S = float.Parse(data.AIDataF[0]);
                Hold5_2BCTR = float.Parse(data.AIDataF[1]);

                Draft_1P = float.Parse(data.AIDataF[2]);
                Draft_2P = float.Parse(data.AIDataF[3]);
                Draft_1SB = float.Parse(data.AIDataF[4]);
                Draft_2SB = float.Parse(data.AIDataF[5]);

                //SUDUT  AI
                sldTrim = float.Parse(data.AIDataF[6]);
                sldList = float.Parse(data.AIDataF[7]);
                //---------------------------

                //LOAD CELL
                AF1P = float.Parse(data.AIDataG[2]);
                AF1SB = float.Parse(data.AIDataG[3]);
                AF2P = float.Parse(data.AIDataG[4]);
                AF2SB = float.Parse(data.AIDataG[5]);

                //level akuarium
                lvAkuariumF = float.Parse(data.AIDataG[6]);
                lvAkuariumB = float.Parse(data.AIDataG[7]);

				//----------------------------

		}

		void RollPitch ()//
		{
           shipVessel.transform.localEulerAngles = new Vector3(Konv_Sudut(sldList), 0, Konv_Sudut(sldTrim));
		//shipVessel.transform.localEulerAngles = new Vector3(-Konv_Sudut(sldList+0.042f), 0, Konv_Sudut(sldTrim+0.051f));
		}
	
		void cargoIsi ()//fungsi untuk isi cargo AI
		{

            cargoList[0].transform.localScale = new Vector3(1, Tray1 / 10, 1);
            cargoList[1].transform.localScale = new Vector3(1, Tray1 / 10, 1);
            cargoList[2].transform.localScale = new Vector3(1, Tray1 / 10, 1);
            cargoList[3].transform.localScale = new Vector3(1, Tray1 / 10, 1);
            cargoList[4].transform.localScale = new Vector3(1, Tray1 / 10, 1);
            cargoList[5].transform.localScale = new Vector3(1, Tray1 / 10, 1);

            cargoList[6].transform.localScale = new Vector3(1, Tray2 / 10, 1);
            cargoList[7].transform.localScale = new Vector3(1, Tray2 / 10, 1);
            cargoList[8].transform.localScale = new Vector3(1, Tray2 / 10, 1);
            cargoList[9].transform.localScale = new Vector3(1, Tray2 / 10, 1);
            cargoList[10].transform.localScale = new Vector3(1, Tray2 / 10, 1);
            cargoList[11].transform.localScale = new Vector3(1, Tray2 / 10, 1);

            cargoList[12].transform.localScale = new Vector3(1, Tray3 / 10, 1);
            cargoList[13].transform.localScale = new Vector3(1, Tray3 / 10, 1);
            cargoList[14].transform.localScale = new Vector3(1, Tray3 / 10, 1);
            cargoList[15].transform.localScale = new Vector3(1, Tray3 / 10, 1);
            cargoList[16].transform.localScale = new Vector3(1, Tray3 / 10, 1);
            cargoList[17].transform.localScale = new Vector3(1, Tray3 / 10, 1);

            cargoList[18].transform.localScale = new Vector3(1, Tray4 / 10, 1);
            cargoList[19].transform.localScale = new Vector3(1, Tray4 / 10, 1);
            cargoList[20].transform.localScale = new Vector3(1, Tray4 / 10, 1);
            cargoList[21].transform.localScale = new Vector3(1, Tray4 / 10, 1);
            cargoList[22].transform.localScale = new Vector3(1, Tray4 / 10, 1);
            cargoList[23].transform.localScale = new Vector3(1, Tray4 / 10, 1);

            cargoList[24].transform.localScale = new Vector3(1, Tray5 / 10, 1);
            cargoList[25].transform.localScale = new Vector3(1, Tray5 / 10, 1);
            cargoList[26].transform.localScale = new Vector3(1, Tray5 / 10, 1);
            cargoList[27].transform.localScale = new Vector3(1, Tray5 / 10, 1);
            cargoList[28].transform.localScale = new Vector3(1, Tray5 / 10, 1);
            cargoList[29].transform.localScale = new Vector3(1, Tray5 / 10, 1);

		}

     

        public float DraftKiriDepan(float v_in)//draft
        {

            //D1P = 4.2377f * Draft_1P * Draft_1P - 12.953f * Draft_1P + 13.039f;
            //D1S = 4.2377f * Draft_1SB * Draft_1SB - 12.953f * Draft_1SB + 13.039f;//3.0695f * Draft_1SB * Draft_1SB - 5.4894f * Draft_1SB + 5.041f;
            //D2P = 0.8716f * Draft_2P * Draft_2P + 1.7135f * Draft_2P - 3.3736f;
            //D2S = 3.9272f * Draft_2SB * Draft_2SB - 11.831f * Draft_2SB + 11.449f;
            float draft_;


            #region Kalibrasi draft kiri depan
            if (v_in <= 2.175f)
            {
                draft_ = 8.363f * v_in - 9.1895f;
            }
            else if (v_in <= 2.31f)
            {
                draft_ = 7.2993f * v_in - 6.8759f;
            }
            else if (v_in <= 2.419f)
            {
                draft_ = 9.3458f * v_in - 11.607f;
            }
            else
            {
                draft_ = 8.8496f * v_in - 10.407f;
            }

            //draft_ = 12.083f * v_in * v_in * v_in - 74.94f * v_in * v_in + 159.04f * v_in - 111.01f;

            #endregion

            return draft_;

        }

        public float DraftKiriBelakang(float v_in)
        {
            float draft_;

            #region Kalibrasi draft kiri belakang

            if (v_in <= 2.224f)
            {
                draft_ = 5.5556f * v_in - 3.3556f;
            }
            else if (v_in <= 2.351f)
            {
                draft_ = 7.874f * v_in - 8.5118f;
            }
            else if (v_in <= 2.486f)
            {
                draft_ = 8.547f * v_in - 10.094f;
            }
            else
            {
                draft_ = 7.874f * v_in - 8.4331f;
            }

            #endregion

            return draft_;


        }

        public float DraftKananDepan(float v_in)
        {
            float draft_;

            #region Kalibrasi draft kiri belakang

            if (v_in <= 2.214f)
            {
                draft_ = 8.1833f * v_in - 9.1178f;
            }
            else if (v_in <= 2.346f)
            {
                draft_ = 7.5758f * v_in - 7.7727f;
            }
            else if (v_in <= 2.458f)
            {
                draft_ = 8.9286f * v_in - 10.946f;
            }
            else
            {
                draft_ = 7.5758f * v_in - 7.6212f;
            }

            #endregion

            return draft_;

        }

        public float DraftKananBelakang(float v_in)
        {
            float draft_;

            #region Kalibrasi draft kiri belakang

            if (v_in <= 2.253f)
            {
                draft_ = 7.9365f * v_in - 8.881f;
            }
            else if (v_in <= 2.38f)
            {
                draft_ = 7.874f * v_in - 8.7402f;
            }
            else if (v_in <= 2.502f)
            {
                draft_ = 8.1967f * v_in - 9.5082f;
            }
            else
            {
                draft_ = 7.2993f * v_in - 7.2628f;
            }

            #endregion

            return draft_;


        }

		void DraftSim ()//draft
		{
		
               
            D1P = DraftKiriDepan(Draft_1P);
            D2P = DraftKiriBelakang(Draft_2P);
            D1S = DraftKananDepan(Draft_1SB);
            D2S = DraftKananBelakang(Draft_2SB);


            tDraft = (D1P + D2P + D1S + D2S) / 4;


           

          shipVessel.transform.position = new Vector3(0, (-dDraftVal + 15) / 4, 0);
        

            
		}

		void ballastIsi ()//fungsi untuk isi cargo
		{

				sideTank [0].transform.localScale = new Vector3 (0, 0, ST_1P);
                sideTank[1].transform.localScale = new Vector3(0, 0, ST_1SB);

				sideTank [2].transform.localScale = new Vector3 (0, 0, ST_2P);
                sideTank[3].transform.localScale = new Vector3(0, 0, ST_2SB);

				sideTank [4].transform.localScale = new Vector3 (0, 0, ST_3P);
                sideTank[5].transform.localScale = new Vector3(0, 0, ST_3SB);

				sideTank [6].transform.localScale = new Vector3 (0, 0,ST_4P);
                sideTank[7].transform.localScale = new Vector3(0, 0, ST_4SB);

				sideTank [8].transform.localScale = new Vector3 (0, 0, ST_5P);
				sideTank [9].transform.localScale = new Vector3 (0, 0, ST_5SB);
          
			    sideTank [10].transform.localScale = new Vector3 (1, VWBF, 1);
				

		}

		void waterDB ()//rumus double bottom low high
		{
           
				//-----------------------------------
                doubleBottom[0].transform.localScale = new Vector3(1, 1, DB_1P);
                doubleBottom[1].transform.localScale = new Vector3(1, 1, DB_1SB);
                doubleBottom[2].transform.localScale = new Vector3(1, 1, DB_2P);
                doubleBottom[3].transform.localScale = new Vector3(1, 1, DB_2SB);
                doubleBottom[4].transform.localScale = new Vector3(1, 1, DB_3P);
                doubleBottom[5].transform.localScale = new Vector3(1, 1, DB_3SB);
                doubleBottom[6].transform.localScale = new Vector3(1, 1, DB_4P);
                doubleBottom[7].transform.localScale = new Vector3(1, 1, DB_4SB);
                doubleBottom[8].transform.localScale = new Vector3(1, 1, DB_5P);
                doubleBottom[9].transform.localScale = new Vector3(1, 1, DB_5SB);
		}
	
		private float  Konv_Sudut (float v_in)
		{
				float v_min, v_max, sudut_min, sudut_max, sudut;

				v_min = 0;
				v_max = 5;

				sudut_min = 90;//spek sensor +-15 degree
				sudut_max = -90;

				sudut = ((sudut_max - sudut_min) / (v_max - v_min)) * (v_in - v_min) + sudut_min;

				return sudut;
		}

		private float conVoltToGrm (float v_in)
		{
				float vMin, vMax, wMin, wMax, weight;

				vMin = 0f; //volt
				vMax = 5f; //volt

				wMin = 0f; //kg
				wMax = 0.25f;//kg

				weight = ((wMax - wMin) / (vMax - vMin)) * (v_in - vMin) + wMin;
				return weight;
		}//fungsi untuk konversi tegangan ke gram

		public float levelAkuarium (float voltIn)
		{
				float lvAkuarium = 20 * voltIn + 10.7f;//(4.0441f * voltIn + 3.7969f) * 2.54f;

		if (voltIn <= 0.137f) {
			
			lvAkuarium = 0.0f;
		}
		
				return lvAkuarium;
		
		}

		public float TuasKiriBelakang (float voltIn)
		{

				float v3 = voltIn * voltIn * voltIn;
				float v2 = voltIn * voltIn;
				float v1 = voltIn;
		
				float vKGF = -0.0017f * v3 + 0.0133f * v2 + 4.9987f * v1 - 0.1167f;

				if (voltIn <= 0.216f) {
			
						vKGF = 0.0f;
				}
		
				return vKGF;
		}

        public float TuasKiriDepan(float voltIn)
        {

            float v3 = voltIn * voltIn * voltIn;
            float v2 = voltIn * voltIn;
            float v1 = voltIn;

            float vKGF = 0.0043f * v3 + 0.0256f * v2 + 5.0273f * v1 - 0.0531f;

            if (voltIn <= 0.196f)
            {

                vKGF = 0.0f;
            }

            return vKGF;
        }

		public float TuasKananBelakang (float voltIn)
		{
		
				float v3 = voltIn * voltIn * voltIn;
				float v2 = voltIn * voltIn;
				float v1 = voltIn;
		
				float vKGF = 0.0029f * v3 - 0.0278f * v2 + 5.0773f * v1 - 0.0678f;
		
				if (voltIn <= 0.216f) {
			
						vKGF = 0.0f;
				}
		
				return vKGF;
		}

        public float TuasKananDepan(float voltIn)
        {

            float v3 = voltIn * voltIn * voltIn;
            float v2 = voltIn * voltIn;
            float v1 = voltIn;

            float vKGF = 0.0027f * v3 - 0.0218f * v2 + 5.0471f * v1 - 0.0376f;

            if (voltIn <= 0.216f)
            {

                vKGF = 0.0f;
            }

            return vKGF;
        }

		void activeBallastTank ()
		{

				// ---------SIDE TANK
				GUI.Label (new Rect (20, 50, 100, 30), "WB Front");
                VWBF = GUI.HorizontalSlider(new Rect(80, 55, 100, 30), VWBF, minIn, maxIn);
                GUI.Label(new Rect(200, 50, 100, 30), "" + VWBF.ToString("#0.000"));

				GUI.Label (new Rect (280, 50, 100, 30), "WB AFT Low");
                VAWBLO = GUI.HorizontalSlider(new Rect(345, 55, 100, 30), VAWBLO, minIn, maxIn);
                GUI.Label(new Rect(460, 50, 100, 30), "" + VAWBLO.ToString("#0.000"));
       
				//----------------------------------------------------------------------------------------------------
				GUI.Label (new Rect (20, 80, 100, 30), "ST-1P");
				ST_1P = GUI.HorizontalSlider (new Rect (80, 85, 100, 30), ST_1P, minIn, maxIn);
				GUI.Label (new Rect (200, 80, 100, 30), "" + ST_1P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 110, 100, 30), "ST-2P");
				ST_2P = GUI.HorizontalSlider (new Rect (80, 115, 100, 30), ST_2P, minIn, maxIn);
				GUI.Label (new Rect (200, 110, 100, 30), "" + ST_2P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 140, 100, 30), "ST-3P");
				ST_3P = GUI.HorizontalSlider (new Rect (80, 145, 100, 30), ST_3P, minIn, maxIn);
				GUI.Label (new Rect (200, 140, 100, 30), "" + ST_3P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 170, 100, 30), "ST-4P");
				ST_4P = GUI.HorizontalSlider (new Rect (80, 175, 100, 30), ST_4P, minIn, maxIn);
				GUI.Label (new Rect (200, 170, 100, 30), "" + ST_4P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 200, 100, 30), "ST-5P");
				ST_5P = GUI.HorizontalSlider (new Rect (80, 205, 100, 30), ST_5P, minIn, maxIn);
				GUI.Label (new Rect (200, 200, 100, 30), "" + ST_5P.ToString ("#0.000"));


				GUI.Label (new Rect (20, 230, 100, 30), "AFT PK P");
                VAWBP = GUI.HorizontalSlider(new Rect(80, 235, 100, 30), VAWBP, minIn, maxIn);
                GUI.Label(new Rect(200, 230, 100, 30), "" + VAWBP.ToString("#0.000"));

                GUI.Label(new Rect(280, 230, 100, 30), "AFT PK SB");
                VAWBSB = GUI.HorizontalSlider(new Rect(345, 235, 100, 30), VAWBSB, minIn, maxIn);
                GUI.Label(new Rect(460, 230, 100, 30), "" + VAWBSB.ToString("#0.000"));

				//----------------------------------------------------
				GUI.Label (new Rect (280, 80, 100, 30), "ST-1SB");
				ST_1SB = GUI.HorizontalSlider (new Rect (345, 85, 100, 30), ST_1SB, minIn, maxIn);
				GUI.Label (new Rect (460, 80, 100, 30), "" + ST_1SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 110, 100, 30), "ST-2SB");
				ST_2SB = GUI.HorizontalSlider (new Rect (345, 115, 100, 30), ST_2SB, minIn, maxIn);
				GUI.Label (new Rect (460, 110, 100, 30), "" + ST_2SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 140, 100, 30), "ST-3SB");
				ST_3SB = GUI.HorizontalSlider (new Rect (345, 145, 100, 30), ST_3SB, minIn, maxIn);
				GUI.Label (new Rect (460, 140, 100, 30), "" + ST_3SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 170, 100, 30), "ST-4SB");
				ST_4SB = GUI.HorizontalSlider (new Rect (345, 175, 100, 30), ST_4SB, minIn, maxIn);
				GUI.Label (new Rect (460, 170, 100, 30), "" + ST_4SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 200, 100, 30), "ST-5SB");
				ST_5SB = GUI.HorizontalSlider (new Rect (345, 205, 100, 30), ST_5SB, minIn, maxIn);
				GUI.Label (new Rect (460, 200, 100, 30), "" + ST_5SB.ToString ("#0.000"));



				

				//------DOUBLE BOTTOM LOW
				GUI.Label (new Rect (20, 260, 100, 30), "DB-1P");
				DB_1P = GUI.HorizontalSlider (new Rect (80, 265, 100, 30), DB_1P, minW, maxW);
				GUI.Label (new Rect (200, 260, 100, 30), "" + DB_1P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 290, 100, 30), "DB-2P");
				DB_2P = GUI.HorizontalSlider (new Rect (80, 295, 100, 30), DB_2P, minW, maxW);
				GUI.Label (new Rect (200, 290, 100, 30), "" + DB_2P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 320, 100, 30), "DB-3P");
				DB_3P = GUI.HorizontalSlider (new Rect (80, 325, 100, 30), DB_3P, minW, maxW);
				GUI.Label (new Rect (200, 320, 100, 30), "" + DB_3P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 350, 100, 30), "DB-4P");
				DB_4P = GUI.HorizontalSlider (new Rect (80, 355, 100, 30), DB_4P, minW, maxW);
				GUI.Label (new Rect (200, 350, 100, 30), "" + DB_4P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 380, 100, 30), "DB-5P");
				DB_5P = GUI.HorizontalSlider (new Rect (80, 385, 100, 30), DB_5P, minW, maxW);
				GUI.Label (new Rect (200, 380, 100, 30), "" + DB_5P.ToString ("#0.000"));


				GUI.Label (new Rect (280, 260, 100, 30), "DB-1SB");
				DB_1SB = GUI.HorizontalSlider (new Rect (345, 265, 100, 30), DB_1SB, minW, maxW);
				GUI.Label (new Rect (460, 260, 100, 30), "" + DB_1SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 290, 100, 30), "DB-2SB");
				DB_2SB = GUI.HorizontalSlider (new Rect (345, 295, 100, 30), DB_2SB, minW, maxW);
				GUI.Label (new Rect (460, 290, 100, 30), "" + DB_2SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 320, 100, 30), "DB-3SB");
				DB_3SB = GUI.HorizontalSlider (new Rect (345, 325, 100, 30), DB_3SB, minW, maxW);
				GUI.Label (new Rect (460, 320, 100, 30), "" + DB_3SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 350, 100, 30), "DB-4SB");
				DB_4SB = GUI.HorizontalSlider (new Rect (345, 355, 100, 30), DB_4SB, minW, maxW);
				GUI.Label (new Rect (460, 350, 100, 30), "" + DB_4SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 380, 100, 30), "DB-5SB");
				DB_5SB = GUI.HorizontalSlider (new Rect (345, 385, 100, 30), DB_5SB, minW, maxW);
				GUI.Label (new Rect (460, 380, 100, 30), "" + DB_5SB.ToString ("#0.000"));


			
             
				if (GUI.Button (new Rect (410, 560, 70, 30), "Close")) {
						menuSim = false;
				}
				if (GUI.Button (new Rect (20, 560, 70, 30), "Reset")) {
						ResetSim ();
				}



		}

		void activeCargoTank ()
		{

            

				// Rumus();
				GUI.Label (new Rect (20, 50, 100, 30), "Trim");

                sldTrim = float.Parse(editTrim);
                editTrim = GUI.TextField(new Rect(80, 55, 100, 20), editTrim, 25);

				//sldTrim = GUI.HorizontalSlider (new Rect (80, 55, 100, 30), sldTrim, minAngle, maxAngle);
				GUI.Label (new Rect (200, 50, 100, 30), "" + sldTrim.ToString ("#0.000"));
		
				GUI.Label (new Rect (280, 50, 100, 30), "List");
				//sldList = GUI.HorizontalSlider (new Rect (345, 55, 100, 30), float.Parse(stringToEdit), minAngle, maxAngle);
                sldList = float.Parse(editList);
                editList = GUI.TextField(new Rect(345, 55, 100, 20), editList, 25);
                
				GUI.Label (new Rect (460, 50, 100, 30), "" + sldList.ToString ("#0.000"));

				//----------------------------------------------------------------------------------------------------
                GUI.Label(new Rect(20, 80, 100, 30), "Hold1_1FCTR");
				Hold1_1FCTR = GUI.HorizontalSlider (new Rect (80, 85, 100, 30), Hold1_1FCTR, minIn, maxIn);
				GUI.Label (new Rect (200, 80, 100, 30), "" + Hold1_1FCTR.ToString ("#0.000"));
				
                GUI.Label (new Rect (20, 110, 100, 30), "Hold1 2P");
				Hold1_2P = GUI.HorizontalSlider (new Rect (80, 115, 100, 30), Hold1_2P, minIn, maxIn);
				GUI.Label (new Rect (200, 110, 100, 30), "" + Hold1_2P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 140, 100, 30), "Hold2 1P");
				Hold2_1P = GUI.HorizontalSlider (new Rect (80, 145, 100, 30), Hold2_1P, minIn, maxIn);
				GUI.Label (new Rect (200, 140, 100, 30), "" + Hold2_1P.ToString ("#0.000"));
				
                GUI.Label (new Rect (20, 170, 100, 30), "Hold2 2P");
				Hold2_2P = GUI.HorizontalSlider (new Rect (80, 175, 100, 30), Hold2_2P, minIn, maxIn);
				GUI.Label (new Rect (200, 170, 100, 30), "" + Hold2_2P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 200, 100, 30), "Hold3 1P");
				Hold3_1P = GUI.HorizontalSlider (new Rect (80, 205, 100, 30), Hold3_1P, minIn, maxIn);
				GUI.Label (new Rect (200, 200, 100, 30), "" + Hold3_1P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 230, 100, 30), "Hold3 2P");
				Hold3_2P = GUI.HorizontalSlider (new Rect (80, 235, 100, 30), Hold3_2P, minIn, maxIn);
				GUI.Label (new Rect (200, 230, 100, 30), "" + Hold3_2P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 260, 100, 30), "Hold4 1P");
				Hold4_1P = GUI.HorizontalSlider (new Rect (80, 265, 100, 30), Hold4_1P, minIn, maxIn);
				GUI.Label (new Rect (200, 260, 100, 30), "" + Hold4_1P.ToString ("#0.000"));
				
                GUI.Label (new Rect (20, 290, 100, 30), "Hold4 2P");
				Hold4_2P = GUI.HorizontalSlider (new Rect (80, 295, 100, 30), Hold4_2P, minIn, maxIn);
				GUI.Label (new Rect (200, 290, 100, 30), "" + Hold4_2P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 320, 100, 30), "Hold5 1P");
				Hold5_1P = GUI.HorizontalSlider (new Rect (80, 325, 100, 30), Hold5_1P, minIn, maxIn);
				GUI.Label (new Rect (200, 320, 100, 30), "" + Hold5_1P.ToString ("#0.000"));
                GUI.Label(new Rect(20, 350, 100, 30), "Hold5_2BCTR");
				Hold5_2BCTR = GUI.HorizontalSlider (new Rect (80, 355, 100, 30), Hold5_2BCTR, minIn, maxIn);
				GUI.Label (new Rect (200, 350, 100, 30), "" + Hold5_2BCTR.ToString ("#0.000"));
		
				//----------------------------------------------------
				GUI.Label (new Rect (280, 80, 100, 30), "Hold1 1SB");
				Hold1_1S = GUI.HorizontalSlider (new Rect (345, 85, 100, 30), Hold1_1S, minIn, maxIn);
				GUI.Label (new Rect (460, 80, 100, 30), "" + Hold1_1S.ToString ("#0.000"));
				GUI.Label (new Rect (280, 110, 100, 30), "Hold1 2SB");
				Hold1_2S = GUI.HorizontalSlider (new Rect (345, 115, 100, 30), Hold1_2S, minIn, maxIn);
				GUI.Label (new Rect (460, 110, 100, 30), "" + Hold1_2S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 140, 100, 30), "Hold2 1SB");
				Hold2_1S = GUI.HorizontalSlider (new Rect (345, 145, 100, 30), Hold2_1S, minIn, maxIn);
				GUI.Label (new Rect (460, 140, 100, 30), "" + Hold2_1S.ToString ("#0.000"));
				GUI.Label (new Rect (280, 170, 100, 30), "Hold2 2SB");
				Hold2_2S = GUI.HorizontalSlider (new Rect (345, 175, 100, 30), Hold2_2S, minIn, maxIn);
				GUI.Label (new Rect (460, 170, 100, 30), "" + Hold2_2S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 200, 100, 30), "Hold3 1SB");
				Hold3_1S = GUI.HorizontalSlider (new Rect (345, 205, 100, 30), Hold3_1S, minIn, maxIn);
				GUI.Label (new Rect (460, 200, 100, 30), "" + Hold3_1S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 230, 100, 30), "Hold3 2SB");
				Hold3_2S = GUI.HorizontalSlider (new Rect (345, 235, 100, 30), Hold3_2S, minIn, maxIn);
				GUI.Label (new Rect (460, 230, 100, 30), "" + Hold3_2S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 260, 100, 30), "Hold4 1SB");
				Hold4_1S = GUI.HorizontalSlider (new Rect (345, 265, 100, 30), Hold4_1S, minIn, maxIn);
				GUI.Label (new Rect (460, 260, 100, 30), "" + Hold4_1S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 290, 100, 30), "Hold4 2SB");
				Hold4_2S = GUI.HorizontalSlider (new Rect (345, 295, 100, 30), Hold4_2S, minIn, maxIn);
				GUI.Label (new Rect (460, 290, 100, 30), "" + Hold4_2S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 320, 100, 30), "Hold5 1SB");
				Hold5_1S = GUI.HorizontalSlider (new Rect (345, 325, 100, 30), Hold5_1S, minIn, maxIn);
				GUI.Label (new Rect (460, 320, 100, 30), "" + Hold5_1S.ToString ("#0.000"));

				GUI.Label (new Rect (280, 350, 100, 30), "Hold5 2SB");
				Hold5_2S = GUI.HorizontalSlider (new Rect (345, 355, 100, 30), Hold5_2S, minIn, maxIn);
				GUI.Label (new Rect (460, 350, 100, 30), "" + Hold5_2S.ToString ("#0.000"));


			
				if (GUI.Button (new Rect (410, 480, 70, 30), "Close")) {
						menuSim = false;
				}
				if (GUI.Button (new Rect (20, 480, 70, 30), "Reset")) {

						ResetSim ();
				}

		}

		void activeDraft ()
		{
            
				GUI.Label (new Rect (20, 80, 100, 30), "Draft 1P");
				Draft_1P = GUI.HorizontalSlider (new Rect (80, 85, 100, 30), Draft_1P, minIn, maxIn);
				GUI.Label (new Rect (200, 80, 100, 30), "" + Draft_1P.ToString ("#0.000"));

				GUI.Label (new Rect (20, 110, 100, 30), "Draft 2P");
				Draft_2P = GUI.HorizontalSlider (new Rect (80, 115, 100, 30), Draft_2P, minIn, maxIn);
				GUI.Label (new Rect (200, 110, 100, 30), "" + Draft_2P.ToString ("#0.000"));



				GUI.Label (new Rect (280, 80, 100, 30), " Draft 1SB");
				Draft_1SB = GUI.HorizontalSlider (new Rect (345, 85, 100, 30), Draft_1SB, minIn, maxIn);
				GUI.Label (new Rect (460, 80, 100, 30), "" + Draft_1SB.ToString ("#0.000"));

				GUI.Label (new Rect (280, 110, 100, 30), " Draft 2SB");
				Draft_2SB = GUI.HorizontalSlider (new Rect (345, 115, 100, 30), Draft_2SB, minIn, maxIn);
				GUI.Label (new Rect (460, 110, 100, 30), "" + Draft_2SB.ToString ("#0.000"));

				if (GUI.Button (new Rect (410, 480, 70, 30), "Close")) {
						menuSim = false;
				}
				if (GUI.Button (new Rect (20, 480, 70, 30), "Reset")) {
						ResetSim ();
				}
		}

    void waterTankF()
    {

        cargoList[0].SetActive(false);
        cargoList[1].SetActive(false);
        cargoList[2].SetActive(false);
        cargoList[3].SetActive(false);
        cargoList[4].SetActive(false);
        cargoList[5].SetActive(false);
        cargoList[6].SetActive(false);
        cargoList[7].SetActive(false);
        cargoList[8].SetActive(false);
        cargoList[9].SetActive(false);
        cargoList[10].SetActive(false);
        cargoList[11].SetActive(false);
        cargoList[12].SetActive(false);
        cargoList[13].SetActive(false);
        cargoList[14].SetActive(false);
        cargoList[15].SetActive(false);
        cargoList[16].SetActive(false);
        cargoList[17].SetActive(false);
        cargoList[18].SetActive(false);
        cargoList[19].SetActive(false);
        cargoList[20].SetActive(false);
        cargoList[21].SetActive(false);
        cargoList[22].SetActive(false);
        cargoList[23].SetActive(false);
        cargoList[24].SetActive(false);
        cargoList[25].SetActive(false);
        cargoList[26].SetActive(false);
        cargoList[27].SetActive(false);
        cargoList[28].SetActive(false);
        cargoList[29].SetActive(false);


    }

		void waterBallastF ()
		{

				sideTank [0].SetActive(false);
				sideTank [1].SetActive(false);
				sideTank [2].SetActive(false);
				sideTank [3].SetActive(false);
				sideTank [4].SetActive(false);
				sideTank [5].SetActive(false);
				sideTank [6].SetActive(false);
				sideTank [7].SetActive(false);
				sideTank [8].SetActive(false);
				sideTank [9].SetActive(false);

				sideTank [10].SetActive(false);
				//sideTank [11].SetActive(false);

				//sideTank [12].SetActive(false);
				//sideTank [13].SetActive(false);

				//sideTank [14].SetActive(false);
				//sideTank [15].SetActive(false);

				doubleBottom [0].SetActive(false);
				doubleBottom [1].SetActive(false);
				doubleBottom [2].SetActive(false);
				doubleBottom [3].SetActive(false);

				doubleBottom[4].SetActive(false);
                doubleBottom[5].SetActive(false);
                doubleBottom[6].SetActive(false);
                doubleBottom[7].SetActive(false);
                doubleBottom[8].SetActive(false);
                doubleBottom[9].SetActive(false);


		}

		void waterTankT ()
		{
            cargoList[0].SetActive(true);
            cargoList[1].SetActive(true);
            cargoList[2].SetActive(true);
            cargoList[3].SetActive(true);
            cargoList[4].SetActive(true);
            cargoList[5].SetActive(true);
            cargoList[6].SetActive(true);
            cargoList[7].SetActive(true);
            cargoList[8].SetActive(true);
            cargoList[9].SetActive(true);
            cargoList[10].SetActive(true);
            cargoList[11].SetActive(true);
            cargoList[12].SetActive(true);
            cargoList[13].SetActive(true);
            cargoList[14].SetActive(true);
            cargoList[15].SetActive(true);
            cargoList[16].SetActive(true);
            cargoList[17].SetActive(true);
            cargoList[18].SetActive(true);
            cargoList[19].SetActive(true);
            cargoList[20].SetActive(true);
            cargoList[21].SetActive(true);
            cargoList[22].SetActive(true);
            cargoList[23].SetActive(true);
            cargoList[24].SetActive(true);
            cargoList[25].SetActive(true);
            cargoList[26].SetActive(true);
            cargoList[27].SetActive(true);
            cargoList[28].SetActive(true);
            cargoList[29].SetActive(true);

		}

		void waterBallastT ()
		{

				sideTank [0].SetActive(true);
				sideTank [1].SetActive(true);
				sideTank [2].SetActive(true);
				sideTank [3].SetActive(true);
				sideTank [4].SetActive(true);
				sideTank [5].SetActive(true);
				sideTank [6].SetActive(true);
				sideTank [7].SetActive(true);
				sideTank [8].SetActive(true);
				sideTank [9].SetActive(true);

				sideTank [10].SetActive(true);
				//sideTank [11].SetActive(true);

				//sideTank [12].SetActive(true);
				//sideTank [13].SetActive(true);
		
				//sideTank [14].SetActive(true);
				//sideTank [15].SetActive(true);

				doubleBottom [0].SetActive(true);
				doubleBottom [1].SetActive(true);
				doubleBottom [2].SetActive(true);
				doubleBottom [3].SetActive(true);
				doubleBottom [4].SetActive(true);
                doubleBottom[5].SetActive(true);
                doubleBottom[6].SetActive(true);
                doubleBottom[7].SetActive(true);
                doubleBottom[8].SetActive(true);
                doubleBottom[9].SetActive(true);
				//doubleBottom [5].SetActive(true);
				//doubleBottom [6].SetActive(true);
				//doubleBottom [7].SetActive(true);


		}

		void formSimulasi (int windowID)  //----------form simulasi
		{


           
				if (GUI.Button (new Rect (20, 20, 100, 30), "Cargo Hold")) {
						tabBallast = false;
						//lblSideTank.SetActive(false);
						tabTrimHold = true;
						tabDraft = false;
						tabAF = false;
						tabLvAqua = false;
						waterBallastF ();
				}
				if (tabTrimHold) {
						activeCargoTank ();
						waterTankT ();
						//lblCargoTank.SetActive(true);
				}

				if (GUI.Button (new Rect (120, 20, 100, 30), "Ballast Tank")) {
						//lblCargoTank.SetActive(false);
						tabTrimHold = false;
						tabDraft = false;
						tabBallast = true;
						tabAF = false;
						tabLvAqua = false;
						waterTankF ();
						//lblSideTank.SetActive(true);

				}
				if (tabBallast) {
						activeBallastTank ();
						waterBallastT ();
				}


				if (GUI.Button (new Rect (220, 20, 100, 30), "Draft")) {
						tabTrimHold = false;
						tabBallast = false;
						tabDraft = true;
						tabAF = false;
						tabLvAqua = false;
				}
				if (tabDraft) {
						activeDraft ();
						print ("view ballast");
				}
				if (GUI.Button (new Rect (320, 20, 100, 30), "Arm Force")) {
						tabTrimHold = false;
						tabBallast = false;
						tabDraft = false;
						tabAF = true;
						tabLvAqua = false;
			
				}
				if (tabAF) {
						activeArmForce ();
			
				}
				if (GUI.Button (new Rect (420, 20, 100, 30), "lvl Akuarium")) {
						tabTrimHold = false;
						tabBallast = false;
						tabDraft = false;
						tabAF = false;
						tabLvAqua = true;
			
			
				}
				if (tabLvAqua) {
						activeAkuarium ();
			
				}


               
				GUI.DragWindow ();
		}

		void activeAkuarium ()
		{
		
				GUI.Label (new Rect (20, 80, 100, 30), "lvl Front");
				lvAkuariumF = GUI.HorizontalSlider (new Rect (80, 85, 100, 30), lvAkuariumF, minIn, maxIn);
				GUI.Label (new Rect (200, 80, 100, 30), "" + lvAkuariumF.ToString ("#0.000"));
		
				GUI.Label (new Rect (20, 110, 100, 30), "lvl Back");
				lvAkuariumB = GUI.HorizontalSlider (new Rect (80, 115, 100, 30), lvAkuariumB, minIn, maxIn);
				GUI.Label (new Rect (200, 110, 100, 30), "" + lvAkuariumB.ToString ("#0.000"));
		
		
				if (GUI.Button (new Rect (410, 480, 70, 30), "Close")) {
						menuSim = false;
				}
				if (GUI.Button (new Rect (20, 480, 70, 30), "Reset")) {
						ResetSim ();
				}
		}

		void activeArmForce ()
		{
		
				GUI.Label (new Rect (20, 80, 100, 30), "AF 1P");
                AF1P = GUI.HorizontalSlider(new Rect(80, 85, 100, 30), AF1P, minIn, maxIn);
				GUI.Label (new Rect (200, 80, 100, 30), "" + AF1P.ToString ("#0.000"));
		
				GUI.Label (new Rect (20, 110, 100, 30), "AF 2P");
				AF2P = GUI.HorizontalSlider (new Rect (80, 115, 100, 30), AF2P, minIn, maxIn);
				GUI.Label (new Rect (200, 110, 100, 30), "" + AF2P.ToString ("#0.000"));
		
				GUI.Label (new Rect (280, 80, 100, 30), " AF 1SB");
				AF1SB = GUI.HorizontalSlider (new Rect (345, 85, 100, 30), AF1SB, minIn, maxIn);
				GUI.Label (new Rect (460, 80, 100, 30), "" + AF1SB.ToString ("#0.000"));
		
				GUI.Label (new Rect (280, 110, 100, 30), " AF 2SB");
				AF2SB = GUI.HorizontalSlider (new Rect (345, 115, 100, 30), AF2SB, minIn, maxIn);
				GUI.Label (new Rect (460, 110, 100, 30), "" + AF2SB.ToString ("#0.000"));
		
		
				if (GUI.Button (new Rect (410, 480, 70, 30), "Close")) {
						menuSim = false;
				}
				if (GUI.Button (new Rect (20, 480, 70, 30), "Reset")) {
						ResetSim ();
				}
		}


        public float KgtoTon(float _kg)
        {
            float _TON = 87 * 87 * 87 * _kg / 1000;
            return _TON;
        }

        public float mmToM(float _mm)
        {
            float _Meter = 87 * _mm / 1000;
            return _Meter;
        }
		void formCallib (int windowID)
		{
            Rumus();
				#region Draft
				GUI.Box (new Rect (20, 30, 500, 25), "Draft");

				GUI.Label (new Rect (20, 60, 150, 30), "Fore Draft Port");
				GUI.TextArea (new Rect (130, 60, 50, 25), "" + D1P.ToString ("F2"));
				GUI.Label (new Rect (180, 60, 150, 30), "m");

				GUI.Label (new Rect (20, 85, 150, 30), "Aft Draft Port");
				GUI.TextArea (new Rect (130, 85, 50, 25), "" + D2P.ToString ("F2"));
				GUI.Label (new Rect (180, 85, 150, 30), "m");

				GUI.Label (new Rect (300, 60, 150, 30), "Fore Draft Starboard");
				GUI.TextArea (new Rect (440, 60, 50, 25), "" + D1S.ToString ("F2"));
				GUI.Label (new Rect (490, 60, 150, 30), "m");

				GUI.Label (new Rect (300, 85, 150, 30), "Aft Draft Starboard");
				GUI.TextArea (new Rect (440, 85, 50, 25), "" + D2S.ToString ("F2"));
				GUI.Label (new Rect (490, 85, 150, 30), "m");

				#endregion

				#region Angle
				GUI.Box (new Rect (20, 120, 500, 25), "Angle");
		
				GUI.Label (new Rect (20, 150, 150, 30), "Trim Angle");
				GUI.TextArea (new Rect (130, 150, 50, 25), "" + dTrimVal.ToString ("F2"));
				GUI.Label (new Rect (180, 150, 150, 30), "deg");
		
				GUI.Label (new Rect (20, 175, 150, 30), "List Angle");
				GUI.TextArea (new Rect (130, 175, 50, 25), "" + Mathf.Abs(dHeelVal).ToString ("F2"));
				GUI.Label (new Rect (180, 175, 150, 30), "deg");
		
				GUI.Label (new Rect (300, 150, 150, 30), "Diff Trim");
				GUI.TextArea (new Rect (440, 150, 50, 25), "" + mmToM(TrimVal) .ToString ("F2"));
				GUI.Label (new Rect (490, 150, 150, 30), "m");
		
				GUI.Label (new Rect (300, 175, 150, 30), "Diff List");
				GUI.TextArea (new Rect (440, 175, 50, 25), "" + mmToM(ListVal).ToString ("F2"));
				GUI.Label (new Rect (490, 175, 150, 30), "m");
		
				#endregion

				#region Arm Force
                //GUI.Box (new Rect (20, 210, 500, 25), "Arm Force");
		
                //GUI.Label (new Rect (20, 240, 150, 30), "Fore AF Port");
                //GUI.TextArea (new Rect (130, 240, 50, 25), "" + TuasKiriDepan (AF1P-1.03f).ToString ("F2"));
                //GUI.Label (new Rect (180, 240, 150, 30), "kgf");
		
                //GUI.Label (new Rect (20, 265, 150, 30), "Aft AF Port");
                //GUI.TextArea (new Rect (130, 265, 50, 25), "" + TuasKiriBelakang (AF2P-0.96f).ToString ("F2"));
                //GUI.Label (new Rect (180, 265, 150, 30), "kgf");
		
                //GUI.Label (new Rect (300, 240, 150, 30), "Fore AF Starboard");
                //GUI.TextArea (new Rect (440, 240, 50, 25), "" + TuasKananDepan (AF1SB-1.05f).ToString ("F2"));
                //GUI.Label (new Rect (490, 240, 150, 30), "kgf");
		
                //GUI.Label (new Rect (300, 265, 150, 30), "Aft AF Starboard");
                //GUI.TextArea (new Rect (440, 265, 50, 25), "" + TuasKananBelakang (AF2SB-1.03f).ToString ("F2"));
                //GUI.Label (new Rect (490, 265, 150, 30), "kgf");

                GUI.Box(new Rect(20, 210, 500, 25), "Arm Force");

                GUI.Label(new Rect(20, 240, 150, 30), "AF Port 1");
                GUI.TextArea(new Rect(130, 240, 50, 25), "" + TuasKiriDepan(AF1P-1.03f).ToString("F2"));
                GUI.Label(new Rect(180, 240, 150, 30), "kgf");

                GUI.TextArea(new Rect(200, 240, 50, 25), "" + (KgtoTon(TuasKiriDepan(AF1P - 1.03f))).ToString("F2"));
                GUI.Label(new Rect(250, 240, 150, 30), "ton");

                GUI.Label(new Rect(20, 265, 150, 30), "AF Port 2");
                GUI.TextArea(new Rect(130, 265, 50, 25), "" + TuasKiriBelakang(AF2P - 0.96f).ToString("F2"));
                GUI.Label(new Rect(180, 265, 150, 30), "kgf");

                GUI.TextArea(new Rect(200, 265, 50, 25), "" + (KgtoTon(TuasKiriDepan(AF2P - 0.96f))).ToString("F2"));
                GUI.Label(new Rect(250, 265, 150, 30), "ton");

                GUI.Label(new Rect(20, 290, 150, 30), "AF SB 1");
                GUI.TextArea(new Rect(130, 290, 50, 25), "" + TuasKananDepan(AF1SB-1.05f).ToString("F2"));
                GUI.Label(new Rect(180, 290, 150, 30), "kgf");

                GUI.TextArea(new Rect(200, 290, 50, 25), "" + (KgtoTon(TuasKiriDepan(AF1SB - 1.05f))).ToString("F2"));
                GUI.Label(new Rect(250, 290, 150, 30), "ton");

                GUI.Label(new Rect(20, 310, 150, 30), "AF SB 2");
                GUI.TextArea(new Rect(130, 310, 50, 25), "" + TuasKananBelakang(AF2SB - 1.03f).ToString("F2"));
                GUI.Label(new Rect(180, 310, 150, 30), "kgf");

                GUI.TextArea(new Rect(200, 310, 50, 25), "" + (KgtoTon(TuasKiriDepan(AF2SB - 1.03f))).ToString("F2"));
                GUI.Label(new Rect(250, 310, 150, 30), "ton");
				#endregion

                //#region Aquarium Water Level
                //GUI.Box (new Rect (20, 300, 500, 25), "Aquarium Water Level");
		
                //GUI.Label (new Rect (20, 330, 150, 30), "Level Front");
                //GUI.TextArea (new Rect (130, 330, 50, 25), "" + (levelAkuarium (lvAkuariumF)/2).ToString ("F2"));
                //GUI.Label (new Rect (180, 330, 150, 30), "cm");
		
                //GUI.Label (new Rect (300, 330, 150, 30), "Level Back");
                //GUI.TextArea (new Rect (440, 330, 50, 25), "" + (levelAkuarium (lvAkuariumB)/2).ToString ("F2"));
                //GUI.Label (new Rect (490, 330, 150, 30), "cm");
		
                //#endregion

				#region Cargo Compartement
                //CargoHold();
                //GUI.Box (new Rect (20, 370, 500, 25), "Cargo Compartement");

                //GUI.Label (new Rect (20, 400, 150, 30), "Hold 1");
                //GUI.TextArea (new Rect (130, 400, 50, 25), "" + (Tray1).ToString ("F2"));
                //GUI.Label (new Rect (180, 400, 150, 30), "KG");

                //GUI.Label (new Rect (20, 425, 150, 30), "Hold 2");
                //GUI.TextArea (new Rect (130, 425, 50, 25), "" + (Tray2).ToString ("F2"));
                //GUI.Label (new Rect (180, 425, 150, 30), "KG");

                //GUI.Label (new Rect (20, 450, 150, 30), "Hold 3");
                //GUI.TextArea (new Rect (130, 450, 50, 25), "" + (Tray3).ToString ("F2"));
                //GUI.Label (new Rect (180, 450, 150, 30), "KG");

                //GUI.Label (new Rect (20, 475, 150, 30), "Hold 4");
                //GUI.TextArea (new Rect (130, 475, 50, 25), "" + (Tray4).ToString ("F2"));
                //GUI.Label (new Rect (180, 475, 150, 30), "KG");

                //GUI.Label (new Rect (20, 500, 150, 30), "Hold 5");
                //GUI.TextArea (new Rect (130, 500, 50, 25), "" + (Tray5).ToString ("F2"));
                //GUI.Label (new Rect (180, 500, 150, 30), "KG");

                GUI.Box(new Rect(20, 370, 500, 25), "Cargo Compartement");

                GUI.Label(new Rect(20, 400, 150, 30), "HOLD 1");
                GUI.TextArea(new Rect(130, 400, 50, 25), "" + (Tray1).ToString("F1"));
                GUI.Label(new Rect(180, 400, 150, 30), "kg");
                GUI.TextArea(new Rect(200, 400, 50, 25), "" + (KgtoTon(Tray1)).ToString("F1"));
                GUI.Label(new Rect(250, 400, 150, 30), "ton");

                GUI.Label(new Rect(20, 430, 150, 30), "HOLD 2");
                GUI.TextArea(new Rect(130, 430, 50, 25), "" + (Tray2).ToString("F1"));
                GUI.Label(new Rect(180, 430, 150, 30), "kg");
                GUI.TextArea(new Rect(200, 430, 50, 25), "" + (KgtoTon(Tray2)).ToString("F1"));
                GUI.Label(new Rect(250, 430, 150, 30), "ton");

                GUI.Label(new Rect(20, 460, 150, 30), "HOLD 3");
                GUI.TextArea(new Rect(130, 460, 50, 25), "" + (Tray3).ToString("F1"));
                GUI.Label(new Rect(180, 460, 150, 30), "kg");
                GUI.TextArea(new Rect(200, 460, 50, 25), "" + (KgtoTon(Tray3)).ToString("F1"));
                GUI.Label(new Rect(250, 460, 150, 30), "ton");

                GUI.Label(new Rect(20, 490, 150, 30), "HOLD 4");
                GUI.TextArea(new Rect(130, 490, 50, 25), "" + (Tray4).ToString("F1"));
                GUI.Label(new Rect(180, 490, 150, 30), "kg");
                GUI.TextArea(new Rect(200, 490, 50, 25), "" + (KgtoTon(Tray4)).ToString("F1"));
                GUI.Label(new Rect(250, 490, 150, 30), "ton");

                GUI.Label(new Rect(20, 520, 150, 30), "HOLD 5");
                GUI.TextArea(new Rect(130, 520, 50, 25), "" + (Tray5).ToString("F1"));
                GUI.Label(new Rect(180, 520, 150, 30), "kg");
                GUI.TextArea(new Rect(200, 520, 50, 25), "" + (KgtoTon(Tray5)).ToString("F1"));
                GUI.Label(new Rect(250, 520, 150, 30), "ton");


				

				#endregion

				#region Ballast Tank
				GUI.Box (new Rect (20, 545, 500, 20), "Ballast Tank");

				GUI.Label (new Rect (20, 565, 150, 30), "WB Fore LVL");
				GUI.TextArea (new Rect (130, 565, 50, 25), "" + VWBF.ToString ("F2"));

				GUI.Label (new Rect (300, 565, 150, 30), "Aft WB Low LVL");
				GUI.TextArea (new Rect (440, 565, 50, 25), "" +  VAWBLO.ToString ("F2"));


				GUI.Label (new Rect (20, 590, 150, 20), "Double Bottom 1P");
				GUI.TextArea (new Rect (130, 590, 50, 20), "" + DB_1P.ToString ("F2"));
				GUI.Label (new Rect (20, 610, 150, 20), "Double Bottom 2P");
				GUI.TextArea (new Rect (130, 610, 50, 20), "" + DB_2P.ToString ("F2"));
				GUI.Label (new Rect (20, 630, 150, 20), "Double Bottom 3P");
				GUI.TextArea (new Rect (130, 630, 50, 20), "" + DB_3P.ToString ("F2"));
				GUI.Label (new Rect (20, 650, 150, 20), "Double Bottom 4P");
				GUI.TextArea (new Rect (130, 650, 50, 20), "" + DB_4P.ToString ("F2"));
				GUI.Label (new Rect (20, 670, 150, 20), "Double Bottom 5P");
				GUI.TextArea (new Rect (130, 670, 50, 20), "" + DB_5P.ToString ("F2"));

				GUI.Label (new Rect (20, 695, 150, 20), "Side Tank 1P");
				GUI.TextArea (new Rect (130, 695, 50, 20), "" + ST_1P.ToString ("F2"));
				GUI.Label (new Rect (20, 715, 150, 20), "Side Tank 2P");
				GUI.TextArea (new Rect (130, 715, 50, 20), "" + ST_2P.ToString ("F2"));
				GUI.Label (new Rect (20, 735, 150, 20), "Side Tank 3P");
				GUI.TextArea (new Rect (130, 735, 50, 20), "" + ST_3P.ToString ("F2"));
				GUI.Label (new Rect (20, 755, 150, 20), "Side Tank 4P");
				GUI.TextArea (new Rect (130, 755, 50, 20), "" + ST_4P.ToString ("F2"));
				GUI.Label (new Rect (20, 775, 150, 20), "Side Tank 5P");
				GUI.TextArea (new Rect (130, 775, 50, 20), "" + ST_5P.ToString ("F2"));
	


				GUI.Label (new Rect (300, 590, 150, 20), "Double Bottom 1SB");
				GUI.TextArea (new Rect (440, 590, 50, 20), "" + DB_1SB.ToString ("F2"));
				GUI.Label (new Rect (300, 610, 150, 20), "Double Bottom 2SB");
				GUI.TextArea (new Rect (440, 610, 50, 20), "" + DB_2SB.ToString ("F2"));
				GUI.Label (new Rect (300, 630, 150, 20), "Double Bottom 3SB");
				GUI.TextArea (new Rect (440, 630, 50, 20), "" + DB_3SB.ToString ("F2"));
				GUI.Label (new Rect (300, 650, 150, 20), "Double Bottom 4SB");
				GUI.TextArea (new Rect (440, 650, 50, 20), "" + DB_4SB.ToString ("F2"));
				GUI.Label (new Rect (300, 670, 150, 20), "Double Bottom 5SB");
				GUI.TextArea (new Rect (440, 670, 50, 20), "" + DB_5SB.ToString ("F2"));

				GUI.Label (new Rect (300, 695, 150, 20), "Side Tank 1SB");
				GUI.TextArea (new Rect (440, 695, 50, 20), "" + ST_1SB.ToString ("F2"));
				GUI.Label (new Rect (300, 715, 150, 20), "Side Tank 2SB");
				GUI.TextArea (new Rect (440, 715, 50, 20), "" + ST_2SB.ToString ("F2"));
				GUI.Label (new Rect (300, 735, 150, 20), "Side Tank 3SB");
				GUI.TextArea (new Rect (440, 735, 50, 20), "" + ST_3SB.ToString ("F2"));
				GUI.Label (new Rect (300, 755, 150, 20), "Side Tank 4SB");
				GUI.TextArea (new Rect (440, 755, 50, 20), "" + ST_4SB.ToString ("F2"));
				GUI.Label (new Rect (300, 775, 150, 20), "Side Tank 5SB");
				GUI.TextArea (new Rect (440, 775, 50, 20), "" + ST_5SB.ToString ("F2"));


				#endregion

               

				GUI.DragWindow ();
            
		}

		
        
		void ResetSim ()
		{

				sldTrim = 2.5f;
				sldList = 2.5f;
		
				//--------------HOLD for sensor	input
				Hold1_1FCTR = 0.0f;
				Hold1_2S = 0.0f;
				Hold1_1FCTR = 0.0f;
				Hold1_2S = 0.0f;

				Hold2_1P = 0.0f;
				Hold2_2S = 0.0f;
				Hold2_1P = 0.0f;
				Hold2_2S = 0.0f;

				Hold3_1P = 0.0f;
				Hold3_2S = 0.0f;
				Hold3_1P = 0.0f;
				Hold3_2S = 0.0f;

				Hold4_1P = 0.0f;
				Hold4_2S = 0.0f;
				Hold4_1P = 0.0f;
				Hold4_2S = 0.0f;

				Hold5_1P = 0.0f;
				Hold5_2S = 0.0f;
				Hold5_1P = 0.0f;
				Hold5_2S = 0.0f;
			
				
			
		}

		void formExit (int windowID)
		{

				
				if (GUI.Button (new Rect (10, 30, 100, 40), "Yes")) {

					
						data.OnApplicationQuit ();
          
						Application.Quit ();
						print ("Keluar");
				}
				if (GUI.Button (new Rect (120, 30, 100, 40), "No")) {
						menuExit = false;
						print ("no");
				}
				GUI.DragWindow ();
		}

		void formMode (int windowID)
		{
		
				if (GUI.Button (new Rect (10, 25, 100, 50), "Solid")) {
						modeNormal ();
						print ("Normal");
				}
		
				if (GUI.Button (new Rect (120, 25, 100, 50), "Transparant")) {
						modeTransparan ();
						print ("Transparan");
				}

			

				GUI.DragWindow ();
		
		}

		void modeTransparan ()
		{
				shipMode.transform.gameObject.GetComponent<Renderer>().material = shader2;
				//shipMode.SetActive(false);
				//waterBallastT ();
				//  obj_Terrain.transform.SetActive(false);
				// water.transform.SetActive(false);
		}

		void modeNormal ()
		{
				shipMode.transform.gameObject.GetComponent<Renderer>().material = shader1;
				//shipMode.SetActive(true);

				//lblCargoTank.SetActive(false);
				//lblSideTank.SetActive(false);
				//waterBallastF ();

				//obj_Terrain.transform.SetActive(true);
				//water.transform.SetActive(true);
				
		}

        #region Bulk Carrier load data, position and cg

        float dWeightLightShip_BC = 29;
        float dWeightTotalLoad_BC = 0;
        float dWeightTotalShip_BC = 0;

        float hSingleLoad_BC = 30; // height of each BC load = 30 mm, 20150830
        float wSingleLoad_BC = 0.2f; // weight of each BC load = 0.2 kg, 20150830

        // lookup table data : iLoadData(row, column), BC Tier data at given bay and row
        // lookup table data : xLoadData(row, column), BC cg_x data at given bay and row
        // lookup table data : yLoadData(row, column), BC cg_y data at given bay and row
        // lookup table data : zLoadData(row, column), BC cg_z data at given bay and row
        // row   = row position, left to right = 1, 0, 2
        // colum = bay position, aft to front = 19, 17, 15, 13, 11, 9, 7, 5, 3, 1
        int iRowPosition_BC = 3;
        int iBayPosition_BC = 10;
        int[,] iLoadDataTable2D_BC = new int[3, 10];
        float[,] xLoadDataTable2D_BC = new float[3, 10] {
            {-641.64f,	-302.64f,	-269f,	-30f,	18.2f,	336.2f,	386f,	643f,	676.86f,	900.86f},
            {-641.64f,	-302.64f,	-269f,	-30f,	18.2f,	336.2f,	386f,	643f,	676.86f,	900.86f},
            {-641.64f,	-302.64f,	-269f,	-30f,	18.2f,	336.2f,	386f,	643f,	676.86f,	900.86f}
        };
        float[,] yLoadDataTable2D_BC = new float[3, 10] {
            {66.785f,	66.785f,	66.785f,	66.785f,	66.785f,	66.785f,	66.785f,	66.785f,	66.785f,	66.785f},
            {0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
            {-66.785f,	-66.785f,	-66.785f,	-66.785f,	-66.785f,	-66.785f,	-66.785f,	-66.785f,	-66.785f,	-66.785f}
        };
        float[,] zLoadDataTable2D_BC = new float[3, 10] {
            {47,	47,	47,	47,	47,	47,	47,	47,	47,	47},
            {47,	47,	47,	47,	47,	47,	47,	47,	47,	47},
            {47,	47,	47,	47,	47,	47,	47,	47,	47,	47}
        };

        #endregion

        #region CG and Attitude Calculation Algorithm
        int[,] iLoadDataTable2D;
        float[,] xLoadDataTable2D;
        float[,] yLoadDataTable2D;
        float[,] zLoadDataTable2D;
        float hSingleLoad; // height of each load in mm, 20150830
        float wSingleLoad; // weight of each load in kg, 20150830
        float dWeightLightShip;
        float dWeightTotalLoad;
        float dWeightTotalShip;

        int iRowPosition;
        int iBayPosition;

       public  float xCGLightShip;
       public float yCGLightShip;
       public float zCGLightShip;
       public float xCGTotalLoad;
       public float yCGTotalLoad;
       public float zCGTotalLoad;
       public float xCGTotalShip;
       public float yCGTotalShip;
       public float zCGTotalShip;

        int iBay19Row3 = 0; // number of tier
        int iBay19Row1 = 0; // number of tier
        int iBay19Row0 = 0; // number of tier
        int iBay19Row2 = 0; // number of tier
        int iBay19Row4 = 0; // number of tier
        int iBay17Row3 = 0; // number of tier
        int iBay17Row1 = 0; // number of tier
        int iBay17Row0 = 0; // number of tier
        int iBay17Row2 = 0; // number of tier
        int iBay17Row4 = 0; // number of tier
        int iBay15Row3 = 0; // number of tier
        int iBay15Row1 = 0; // number of tier
        int iBay15Row0 = 0; // number of tier
        int iBay15Row2 = 0; // number of tier
        int iBay15Row4 = 0; // number of tier
        int iBay13Row3 = 0; // number of tier
        int iBay13Row1 = 0; // number of tier
        int iBay13Row0 = 0; // number of tier
        int iBay13Row2 = 0; // number of tier
        int iBay13Row4 = 0; // number of tier
        int iBay11Row3 = 0; // number of tier
        int iBay11Row1 = 0; // number of tier
        int iBay11Row0 = 0; // number of tier
        int iBay11Row2 = 0; // number of tier
        int iBay11Row4 = 0; // number of tier
        int iBay9Row3 = 0; // number of tier
        int iBay9Row1 = 0; // number of tier
        int iBay9Row0 = 0; // number of tier
        int iBay9Row2 = 0; // number of tier
        int iBay9Row4 = 0; // number of tier
        int iBay7Row3 = 0; // number of tier
        int iBay7Row1 = 0; // number of tier
        int iBay7Row0 = 0; // number of tier
        int iBay7Row2 = 0; // number of tier
        int iBay7Row4 = 0; // number of tier
        int iBay5Row3 = 0; // number of tier
        int iBay5Row1 = 0; // number of tier
        int iBay5Row0 = 0; // number of tier
        int iBay5Row2 = 0; // number of tier
        int iBay5Row4 = 0; // number of tier
        int iBay3Row3 = 0; // number of tier
        int iBay3Row1 = 0; // number of tier
        int iBay3Row0 = 0; // number of tier
        int iBay3Row2 = 0; // number of tier
        int iBay3Row4 = 0; // number of tier
        int iBay1Row3 = 0; // number of tier
        int iBay1Row1 = 0; // number of tier
        int iBay1Row0 = 0; // number of tier
        int iBay1Row2 = 0; // number of tier
        int iBay1Row4 = 0; // number of tier

        int iLoadCount = 0; // total number of container loads

        private void CalculateCG_and_Attitude()
        {
            
                iLoadDataTable2D = (int[,])iLoadDataTable2D_BC.Clone();
                xLoadDataTable2D = (float[,])xLoadDataTable2D_BC.Clone();
                yLoadDataTable2D = (float[,])yLoadDataTable2D_BC.Clone();
                zLoadDataTable2D = (float[,])zLoadDataTable2D_BC.Clone();

                dWeightLightShip = dWeightLightShip_BC;
                dWeightTotalLoad = dWeightTotalLoad_BC;
                dWeightTotalShip = dWeightTotalShip_BC;

                iRowPosition = iRowPosition_BC;
                iBayPosition = iBayPosition_BC;

                hSingleLoad = hSingleLoad_BC; // height of each load in mm, 20150830
                wSingleLoad = wSingleLoad_BC; // weight of each load in kg, 20150830

                iLoadDataTable2D[0, 0] = iBay19Row1;
                iLoadDataTable2D[1, 0] = iBay19Row0;
                iLoadDataTable2D[2, 0] = iBay19Row2;

                iLoadDataTable2D[0, 1] = iBay17Row1;
                iLoadDataTable2D[1, 1] = iBay17Row0;
                iLoadDataTable2D[2, 1] = iBay17Row2;

                iLoadDataTable2D[0, 2] = iBay15Row1;
                iLoadDataTable2D[1, 2] = iBay15Row0;
                iLoadDataTable2D[2, 2] = iBay15Row2;

                iLoadDataTable2D[0, 3] = iBay13Row1;
                iLoadDataTable2D[1, 3] = iBay13Row0;
                iLoadDataTable2D[2, 3] = iBay13Row2;

                iLoadDataTable2D[0, 4] = iBay11Row1;
                iLoadDataTable2D[1, 4] = iBay11Row0;
                iLoadDataTable2D[2, 4] = iBay11Row2;

                iLoadDataTable2D[0, 5] = iBay9Row1;
                iLoadDataTable2D[1, 5] = iBay9Row0;
                iLoadDataTable2D[2, 5] = iBay9Row2;

                iLoadDataTable2D[0, 6] = iBay7Row1;
                iLoadDataTable2D[1, 6] = iBay7Row0;
                iLoadDataTable2D[2, 6] = iBay7Row2;

                iLoadDataTable2D[0, 7] = iBay5Row1;
                iLoadDataTable2D[1, 7] = iBay5Row0;
                iLoadDataTable2D[2, 7] = iBay5Row2;

                iLoadDataTable2D[0, 8] = iBay3Row1;
                iLoadDataTable2D[1, 8] = iBay3Row0;
                iLoadDataTable2D[2, 8] = iBay3Row2;

                iLoadDataTable2D[0, 9] = iBay1Row1;
                iLoadDataTable2D[1, 9] = iBay1Row0;
                iLoadDataTable2D[2, 9] = iBay1Row2;

                xCGLightShip = 0;
                yCGLightShip = 0;
                zCGLightShip = KG_BC_REAL;

            xCGTotalLoad = 0;
            yCGTotalLoad = 0;
            zCGTotalLoad = 0;

            iLoadCount = 0;
            for (int i = 0; i < iRowPosition; i++)
                for (int j = 0; j < iBayPosition; j++)
                {
                    iLoadCount += iLoadDataTable2D[i, j];
                    xCGTotalLoad += xLoadDataTable2D[i, j] * iLoadDataTable2D[i, j];
                    yCGTotalLoad += yLoadDataTable2D[i, j] * iLoadDataTable2D[i, j];
                    zCGTotalLoad += zLoadDataTable2D[i, j] * iLoadDataTable2D[i, j] + 0.5f * hSingleLoad * iLoadDataTable2D[i, j] * iLoadDataTable2D[i, j];
                    //zCGTotalLoad += zLoadDataTable2D[i, j] + 0.5 * hSingleLoad * (iLoadDataTable2D[i, j] - 1);
                }
            if (iLoadCount > 0)
            {
                xCGTotalLoad /= iLoadCount;
                yCGTotalLoad /= iLoadCount;
                zCGTotalLoad /= iLoadCount;
            }
            else
            {
                xCGTotalLoad = 0;
                yCGTotalLoad = 0;
                zCGTotalLoad = 0;
            }
            dWeightTotalLoad = wSingleLoad * iLoadCount;
            dWeightTotalShip = dWeightTotalLoad + dWeightLightShip;

            xCGTotalShip = (xCGTotalLoad * dWeightTotalLoad + xCGLightShip * dWeightLightShip) / (dWeightTotalShip);
            yCGTotalShip = (yCGTotalLoad * dWeightTotalLoad + yCGLightShip * dWeightLightShip) / (dWeightTotalShip);
            zCGTotalShip = (zCGTotalLoad * dWeightTotalLoad + zCGLightShip * dWeightLightShip) / (dWeightTotalShip);

            // calculate orientation: heel and trim angle
            dKBTVal = Interpolate2D(dWeightTotalShip, 0, dispDataTable1D, heelDataTable1D, kbtDataTable2D);
            dKMTVal = Interpolate2D(dWeightTotalShip, 0, dispDataTable1D, heelDataTable1D, kmtDataTable2D);

            dKBLVal = Interpolate2D(dWeightTotalShip, 0, dispDataTable1D, trimDataTable1D, kblDataTable2D);
            dKMLVal = Interpolate2D(dWeightTotalShip, 0, dispDataTable1D, trimDataTable1D, kmlDataTable2D);


            float BMT = dKMTVal - dKBTVal;
            float BML = dKMLVal - dKBLVal;
            _heel = Mathf.Atan(yCGTotalShip / BMT) * 180 / Mathf.PI; // in deg
            _trim = Mathf.Atan(xCGTotalShip / BML) * 180 / Mathf.PI; // in deg

            // calculate transverse and longitudinal stability
            //nudInputVal.Value = (decimal)dWeightTotalShip;
            //nudHeelVal.Value = (decimal)(-heel_angle);
            //nudPitchVal.Value = (decimal)(-trim_angle);
           

            CalculateTranverseHydrostatic();
            CalculateLongHydrostatic();

            // show results
            //nudCGTotalLoadX.Value = (decimal)xCGTotalLoad;
            //nudCGTotalLoadY.Value = (decimal)yCGTotalLoad;
            //nudCGTotalLoadZ.Value = (decimal)zCGTotalLoad;
            //nudCGTotalShipX.Value = (decimal)xCGTotalShip;
            //nudCGTotalShipY.Value = (decimal)yCGTotalShip;
            //nudCGTotalShipZ.Value = (decimal)zCGTotalShip;
            //nudContainerCount.Value = iLoadCount;
            //nudDispTotalLoad.Value = (decimal)dWeightTotalLoad;
            //nudDispTotalShip.Value = (decimal)dWeightTotalShip;

            //txbHeelAngle.Text = heel_angle.ToString("F2");
            //txbTrimAngle.Text = trim_angle.ToString("F2");
        }

        #endregion

}
