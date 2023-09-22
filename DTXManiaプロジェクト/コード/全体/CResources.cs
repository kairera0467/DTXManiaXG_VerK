using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Diagnostics;
using System.IO;


namespace DTXMania
{
	/// <summary>
	/// 多言語メッセージ情報を扱います。
	/// </summary>
	public class CResources
	{
		private const string csvRootPath = @"System\Common\Language"; //Ver.K向けに変更
		public string csvCurrentPath
		{
			get;
			set;
		}
		public void ResetCsvPath()
		{
			csvCurrentPath = csvRootPath;
		}
		private string csvFileName = @"resources.csv";

		private string[] csvHeader = null;
		private Dictionary<string, string> dict = new Dictionary<string, string>();

		private string[] langcodelist = null, langdisplist = null;

		private string strLanguageCode;


		/// <summary>
		/// 表示に使用する言語情報を取得/設定する
		/// 例: Language("ja-JP") など。
		/// </summary>
		public string Language
		{
			get {
				if ( strLanguageCode == "" || strLanguageCode == null )
				{
					string s = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
					if ( s == "" || csvHeader == null || !csvHeader.Contains( s+".title" ) )
					{
						strLanguageCode = "default";
					}
					else
					{
						strLanguageCode = s;
					}

				}
//Debug.WriteLine( "Get: strLanguageCode: " + strLanguageCode );
				return strLanguageCode;
			}
			set
			{
				if ( value == "" || value == null )
				{
					string s = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
					if ( s == "" || csvHeader == null || !csvHeader.Contains( s + ".title" ) )
					{
						strLanguageCode = "default";
					}
					else
					{
						strLanguageCode = s;
					}
				}
				else
				{
					if ( csvHeader == null || !csvHeader.Contains( value + ".title" ) )
					{
						strLanguageCode = "default";
					}
					else
					{
						strLanguageCode = value;
					}
				}
				if ( CDTXMania.ConfigIni != null )
				{
					//CDTXMania.ConfigIni.strLanguage = strLanguageCode;
//Debug.WriteLine( "strLang.Value=" + CDTXMania.Instance.ConfigIni.strLanguage.Value );
				}
//Debug.WriteLine( "Set: strLanguageCode: " + strLanguageCode );
			}
		}

		/// <summary>
		/// 使用可能な言語(表示名)のリストを返す
		/// </summary>
		/// <remarks>
		/// listの格納順は、LanguageCodeListと一致する。
		/// </remarks>
		public string[] LanguageDispList
		{
			get
			{
				return langdisplist;
			}
		}

		/// <summary>
		/// 使用可能な言語(ja-JPなどの言語コード)のリストを返す
		/// </summary>
		/// <remarks>
		/// listの格納順は、LanguageDispListと一致する。
		/// </remarks>
		public string[] LanguageCodeList
		{
			get
			{
				return langcodelist;
			}
		}
		/// <summary>
		/// 現在設定されている言語のindex順を返す
		/// LanguageDispListやLanguageCodeListのindexに相当する
		/// </summary>
		public int LanguageCodeIndex
		{
			get
			{
				int index = Array.IndexOf( langcodelist, strLanguageCode );
				if ( index < 0 ) index = 0;
				return index;
			}
		}
		
		/// <summary>
		/// key名に相当するlabel(表示名)を返す
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string Label( string key )
		{
			return Resource( key, "title", strLanguageCode );
		}
		public string Label( string key, string strLang )
		{
			return Resource( key, "title", strLang );
		}
		/// <summary>
		/// key名に相当するExplanation(説明文)を返す
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string Explanation( string key )
		{
			return Resource( key, "value", strLanguageCode );
		}
		/// <summary>
		/// key名に相当するitem(コンマ区切りの選択肢)を返す
		/// ただし現在は未使用
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string ItemsStr( string key )
		{
			return Resource( key, "items", strLanguageCode );
		}

		/// <summary>
		/// key名に相当するitemの配列を返す
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string[] Items(string key)
		{
			string itemstr = Resource(key, "items", strLanguageCode);
			string[] items = itemstr.Split(',');
			for (int i = 0; i <items.Length; i++)
			{
				items[i] = items[i].Trim();
			}
			return items;

		}

		private string Resource(string key, string strType)
		{
			return Resource( key, strType, strLanguageCode );
		}

		public string Resource( string key, string strType, string strLangCode )
		{
			if (strType != "" && strType != "title" && strType != "value" && strType != "items")
			{
				throw new ArgumentOutOfRangeException( "CResources.Resource: 引数が正しくありません。(" + strType + ")" );
			}
			string key_ = key + "." + strLangCode + "." + strType;
			string value = "";

//Debug.WriteLine( "strLangCode=" + strLangCode );
//Debug.WriteLine( "key_=" + key_ );
			if ( !dict.ContainsKey( key_ ) )				// keyかvalueが存在しない場合
			{
				value = "";
			}
			else
			{
				value = dict[ key_ ];
//Debug.WriteLine( "value =" + value );

				if (value == "")	// もし未定義なら、defaultの文字列にfallbackする
				{
					if ( strLangCode == "default" )
					{
						value = "";
					}
					else
					{
						return Resource( key, strType, "default" );
					}
				}
			}
			return value;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CResources()
		{
			csvCurrentPath = csvRootPath;
//            this.csvPath = excelPath;
		}

		/// <summary>
		/// csvファイルを読み込んで、言語リソースを初期化する。
		/// languageで指定した言語リソースがない場合は、default(=en-US)にフォールバックする。
		/// </summary>
		/// <param name="language">"ja-JP"などの言語情報。""の場合は、default(=en-US)が用いられる。</param>
		public bool LoadResources(string language = "")
        {
			// 参考: http://dobon.net/vb/dotnet/file/readcsvfile.html
			Microsoft.VisualBasic.FileIO.TextFieldParser tfp;
			try
			{
				tfp = new Microsoft.VisualBasic.FileIO.TextFieldParser(
						Path.Combine(csvCurrentPath, csvFileName),
						System.Text.Encoding.Unicode
				);
			}
			catch ( System.IO.FileNotFoundException e )
			{
				Trace.TraceError( "言語情報ファイル System/resources.csv が見つかりませんでした。" + e.Message );
				return false;
			}

			//フィールドが文字で区切られているとする
			//デフォルトでDelimitedなので、必要なし
			tfp.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
			//区切り文字を,とする
			tfp.Delimiters = new string[] { "," };
			//フィールドを"で囲み、改行文字、区切り文字を含めることができるか
			//デフォルトでtrueなので、必要なし
			tfp.HasFieldsEnclosedInQuotes = true;
			//フィールドの前後からスペースを削除しない
			tfp.TrimWhiteSpace = false;

			bool bAlreadyReadFirstLine = false;
			while ( !tfp.EndOfData )
			{
				string[] fields = tfp.ReadFields();

				if ( !bAlreadyReadFirstLine )
				{
					//csvHeader.Add( fields );
					csvHeader = fields;
					bAlreadyReadFirstLine = true;
				}
				else
				{
					string strItemName = fields[ 0 ];
					if (strItemName == "" || strItemName.Contains("/") )
					{
						continue;
					}
					else
					{
						for ( int i = 0; i < fields.GetLength( 0 ); i++ )
						{
							string key = strItemName + "." + csvHeader[ i ];
							string value = fields[ i ];

							value = value.Replace( "\r", "" ).Replace( "\n", "" );		// 文字コードとしての改行は削除して、
							value = value.Replace( "\\n", Environment.NewLine );		// "\n" と書かれたところを改行文字に置換する
							dict[ key ] = value;
						}
					}

				}
			}

			//後始末
			tfp.Close();


			#region [ langcodelist, langlist 生成 ]
			List<string> lstLangCodeList = new List<string>();
			List<string> lstLangDispList = new List<string>();

			for ( int i = 1; i < csvHeader.Length; i++ )		// 0から開始、ではない (0は名称定義)
			{
				string s = csvHeader[ i ].Replace( ".title", "" ).Replace( ".value", "" ).Replace( ".items", "" );
				if ( !lstLangCodeList.Contains( s ) )
				{
					lstLangCodeList.Add( s );
					lstLangDispList.Add( Label("strCfgLanguageName", s ) );
				}
			}
			langcodelist = lstLangCodeList.ToArray();
			langdisplist = lstLangDispList.ToArray();
			#endregion

			Language = language;

			return true;
        }

	
	
		#region [ Dispose-Finallizeパターン実装 ]
		//-----------------
		public void Dispose()
		{
			this.Dispose( true );
			GC.SuppressFinalize( this );
		}
		protected void Dispose( bool bManagedDispose )
		{
			dict = null;
			csvHeader = null;
			langcodelist = null;
			langdisplist = null;
		}
		~CResources()
		{
			this.Dispose( false );
		}
		//-----------------
		#endregion
	}
}
