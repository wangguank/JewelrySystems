﻿using UnityEngine;
using System.Collections;
using JObject = System.Collections.Generic.Dictionary<string, object>;
using System.IO;


namespace Client.Utils.Example
{    
    public class Example_ObjectData : MonoBehaviour
    {
        public UsersData usersData;

        string filePath = "/Addons/Extension/Data-Example";
        void OnGUI() {
            if (GUILayout.Button("保存json数据")) {
                usersData = new UsersData();
                UserInfo userInfo = new UserInfo();

                userInfo.userBaseInfo.firstName = "Coach";
                userInfo.userBaseInfo.email = "Coach@mySwing.com";
                userInfo.userBaseInfo.coachID = userInfo.userBaseInfo.email;


                usersData.DefalutUserID = userInfo.userBaseInfo.email;

                userInfo.userBaseInfo.userType = UserType.Coach;
                usersData.userList.Add(userInfo);
                usersData.CurrentUserID = userInfo.userBaseInfo.email;
                usersData.CurrentApplyUserID = userInfo.userBaseInfo.email;


                UserInfo studentInfo = new UserInfo();
                studentInfo.userBaseInfo.firstName = "Demo";
                studentInfo.userBaseInfo.email = "Demo@mySwing.com";
                studentInfo.userBaseInfo.coachID = userInfo.userBaseInfo.email;

               
                usersData.DefalutUserID = studentInfo.userBaseInfo.email;

                studentInfo.userBaseInfo.userType = UserType.Student;
                usersData.userList.Add(studentInfo);

                usersData.CurrentUserID = studentInfo.userBaseInfo.email;
                usersData.CurrentApplyUserID = studentInfo.userBaseInfo.email;

                SaveUserData();

            }
			if(GUILayout.Button("读取json文件")){
				UsersData usersData = ReadUsersData();
				Debug.LogError ("usersData.CurrentUserID: "+usersData.CurrentUserID);
			}

			if(GUILayout.Button("异步读取json")){
				AsyncReadUsersData(OnUsersDataReaderEventStatus);

			}


        }

        public bool WriteUsersData(UsersData data, string fileName)
        {
			string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath + filePath, fileName);
            JObject json_data = data.Serialize(ClientConfig.DataVersionCode);
            FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);
            return true;
        }


        public void SaveUserData()
        {
            WriteUsersData(usersData, "userinfo");
        }

		public UsersData ReadUsersData()
		{
			JObject json_data = null;

			//检查基本数据文件是否存在，并尝试解析
			string normalFile = string.Format("{0}/{1}.txt", Application.dataPath+ filePath, "userinfo");
			string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath+ filePath, "userinfo");
			#if !CLIENT_WEB
			if (File.Exists(normalFile))
			{
				json_data = FileManager.Instance.ReadJsonObject(normalFile);
			}
			else
			{
				//不存在基本数据文件，则尝试以加密方式解析
				json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);
			}
			#endif
			if (json_data != null)
			{
				//检查数据文件版本号，如果低于当前系统版本，则升级重写
				int vercode = JsonHelper.GetInteger(json_data, ClientConfig.KEY_DATA_VERSION_CODE);

				if (vercode != ClientConfig.DataVersionCode)
				{
					if (json_data.ContainsKey(ClientConfig.KEY_DATA_VERSION_CODE))
					{
						json_data.Remove(ClientConfig.KEY_DATA_VERSION_CODE);
					}
					json_data.Add(ClientConfig.KEY_DATA_VERSION_CODE, ClientConfig.DataVersionCode);
					#if !CLIENT_WEB
					FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

					if (File.Exists(normalFile))
					{
						File.Delete(normalFile);
					}
					#endif
				}
				return new UsersData(json_data, vercode);
			}
			return null;
		}

		/// <summary>
		/// 异步读取文件
		/// </summary>
		/// <param name="configData"></param>
		/// <returns></returns>
		public void AsyncReadUsersData(FileReadTaskEventHandler handler)
		{
			//检查基本数据文件是否存在，并尝试解析
			string normalFile = string.Format("{0}/{1}.txt", Application.dataPath+ filePath, "userinfo");
			string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath+ filePath, "userinfo");
			if (File.Exists(normalFile))
			{
				//检查普通格式文件是否存在，并进行解码
				FileManager.Instance.AsyncReadJsonObject(normalFile, handler);
			}
			else
			{
				//检查是否存在加密数据文件，并进行解码
				FileManager.Instance.AsyncReadEncryptZipJsonObject(encryptFile, handler);
			}
		}

		public void OnUsersDataReaderEventStatus(string fname, FileDataType dtype, FileTaskStatus status, object data)
		{
			//检查是否当前文件

			{
				switch (status)
				{
				case FileTaskStatus.Progressing:
					{
						break;
					}
				case FileTaskStatus.Success:
					{
						
						{
							JObject jdata = data as JObject;
							int vercode = JsonHelper.GetInteger(jdata, ClientConfig.KEY_DATA_VERSION_CODE);	
							UsersData _usersData = new UsersData(jdata, vercode);

							//对旧数据整体迁移加密之后回存；
							if (vercode != ClientConfig.DataVersionCode)
							{

								fname = string.Format("{0}/{1}.txt", Application.dataPath+ filePath, "userinfo");
								if (File.Exists(fname))
								{
									File.Delete(fname);
								}

								fname  = string.Format("{0}/{1}.ts", Application.dataPath+ filePath, "userinfo");


								FileManager.Instance.AsyncWriteEncryptZipJsonObject(fname, _usersData.Serialize(ClientConfig.DataVersionCode), null);
							}
							Debug.LogError("_usersData:"+_usersData.CurrentUserID);
							//ApplicationUI.Instance.closeWindowObject(WINDOWOBJECT_TYPE.PROGRESSWINDOW, SHOW_EFFECT.NONE, true);


						}

						break;
					}
				case FileTaskStatus.Failed:
					{
						//*_*ApplicationUI.Instance.closeWindowObject(WINDOWOBJECT_TYPE.PROGRESSWINDOW, SHOW_EFFECT.NONE, true);
						//*_*PromptManager.Instance.ShowPrompt(5019, null, null, null);
						break;
					}
				}
			}
		}


		/*
		#region save swingMapping
		public HitConfigData ReadHitHistoryConfig(string dirname)
		{
			JObject json_data = null;

			//检查基本数据文件是否存在，并尝试解析
			string normalFile = string.Format("{0}/{1}/{2}.txt", CommonVariable.PersistentHitDataPath, dirname, CommonVariable.SwingMappingFileName);
			string encryptFile = string.Format("{0}/{1}/{2}.ts", CommonVariable.PersistentHitDataPath, dirname, CommonVariable.SwingMappingFileName);
			if (File.Exists(normalFile))
			{
				json_data = FileManager.Instance.ReadJsonObject(normalFile);
			}
			else
			{
				//不存在基本数据文件，则尝试以加密方式解析
				json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);
			}

			if (json_data != null)
			{
				//检查数据文件版本号，如果低于当前系统版本，则升级重写
				int vercode = JsonHelper.GetInteger(json_data, ClientConfig.KEY_DATA_VERSION_CODE);

				if (vercode != ClientConfig.DataVersionCode)
				{
					if (json_data.ContainsKey(ClientConfig.KEY_DATA_VERSION_CODE))
					{
						json_data.Remove(ClientConfig.KEY_DATA_VERSION_CODE);
					}
					json_data.Add(ClientConfig.KEY_DATA_VERSION_CODE, ClientConfig.DataVersionCode);

					FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

					if (File.Exists(normalFile))
					{
						File.Delete(normalFile);
					}
				}
				return new HitConfigData(json_data, vercode);
			}

			return null;

		}

		public HitConfigData ReadAllHitHistoryConfigByDir(string dirname)
		{
			JObject json_data = null;

			//检查基本数据文件是否存在，并尝试解析
			string normalFile = string.Format("{0}/{1}.txt", dirname, CommonVariable.SwingMappingFileName);
			string encryptFile = string.Format("{0}/{1}.ts", dirname, CommonVariable.SwingMappingFileName);
			if (File.Exists(normalFile))
			{
				json_data = FileManager.Instance.ReadJsonObject(normalFile);
			}
			else
			{
				//不存在基本数据文件，则尝试以加密方式解析
				json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);
			}

			if (json_data != null)
			{
				//检查数据文件版本号，如果低于当前系统版本，则升级重写
				int vercode = JsonHelper.GetInteger(json_data, ClientConfig.KEY_DATA_VERSION_CODE);

				if (vercode != ClientConfig.DataVersionCode)
				{
					if (json_data.ContainsKey(ClientConfig.KEY_DATA_VERSION_CODE))
					{
						json_data.Remove(ClientConfig.KEY_DATA_VERSION_CODE);
					}
					json_data.Add(ClientConfig.KEY_DATA_VERSION_CODE, ClientConfig.DataVersionCode);

					FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

					if (File.Exists(normalFile))
					{
						File.Delete(normalFile);
					}
				}
				return new HitConfigData(json_data, vercode);
			}

			return null;

		}

		public HitConfigData ReadBlankHistoryConfig(string dirname)
		{
			JObject json_data = null;

			//检查基本数据文件是否存在，并尝试解析
			string normalFile = string.Format("{0}/{1}/{2}.txt", CommonVariable.PersistentBlankDataPath, dirname, CommonVariable.BlankMappingFileName);
			string encryptFile = string.Format("{0}/{1}/{2}.ts", CommonVariable.PersistentBlankDataPath, dirname, CommonVariable.BlankMappingFileName);
			if (File.Exists(normalFile))
			{
				json_data = FileManager.Instance.ReadJsonObject(normalFile);
			}
			else
			{
				//不存在基本数据文件，则尝试以加密方式解析
				json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);
			}

			if (json_data != null)
			{
				//检查数据文件版本号，如果低于当前系统版本，则升级重写
				int vercode = JsonHelper.GetInteger(json_data, ClientConfig.KEY_DATA_VERSION_CODE);

				if (vercode != ClientConfig.DataVersionCode)
				{
					if (json_data.ContainsKey(ClientConfig.KEY_DATA_VERSION_CODE))
					{
						json_data.Remove(ClientConfig.KEY_DATA_VERSION_CODE);
					}
					json_data.Add(ClientConfig.KEY_DATA_VERSION_CODE, ClientConfig.DataVersionCode);

					FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

					if (File.Exists(normalFile))
					{
						File.Delete(normalFile);
					}
				}
				return new HitConfigData(json_data, vercode);
			}

			return null;

		}

		public bool WriteSwingMapping(HitConfigData data)
		{
			string encryptFile = data.getEncryptSwingMappingFileName();
			JObject json_data = data.Serialize(ClientConfig.DataVersionCode);
			bool result = FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);
			if (result)
			{
				UpdateHitConfigData(data);
			}
			return result;
		}
		public bool WriteBlankSwingMapping(HitConfigData data)
		{
			string encryptFile = data.getEncryptBlankMappingFileName();
			JObject json_data = data.Serialize(ClientConfig.DataVersionCode);
			bool result = FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);
			if (result)
			{

				Blank_UpdateHitConfigData(data);
			}
			return result;
		}

		#endregion

		#region SwingClub Data
		//one swingclub data
		public SwingClubData ReadSwingClubData(HitConfigData configData)
		{
			JObject json_data = null;

			//检查基本数据文件是否存在，并尝试解析
			string normalFile = configData.GetSwingDataFileName();
			string encryptFile = configData.GetEncryptSwingDataFileName();
			if (File.Exists(normalFile))
			{
				json_data = FileManager.Instance.ReadJsonObject(normalFile);
			}
			else
			{
				//不存在基本数据文件，则尝试以加密方式解析
				json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);
			}

			if (json_data != null)
			{
				//检查数据文件版本号，如果低于当前系统版本，则升级重写
				int vercode = JsonHelper.GetInteger(json_data, ClientConfig.KEY_DATA_VERSION_CODE);

				if (vercode != ClientConfig.DataVersionCode)
				{
					if (json_data.ContainsKey(ClientConfig.KEY_DATA_VERSION_CODE))
					{
						json_data.Remove(ClientConfig.KEY_DATA_VERSION_CODE);
					}
					json_data.Add(ClientConfig.KEY_DATA_VERSION_CODE, ClientConfig.DataVersionCode);

					FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

					if (File.Exists(normalFile))
					{
						File.Delete(normalFile);
					}
				}
				return new SwingClubData(json_data, vercode);
			}

			return null;
		}

		/// <summary>
		/// 异步读取文件
		/// </summary>
		/// <param name="configData"></param>
		/// <returns></returns>
		public void AsyncReadSwingClubData(HitConfigData configData, FileReadTaskEventHandler handler)
		{
			string filename = configData.GetSwingDataFileName();
			if (File.Exists(filename))
			{
				//检查普通格式文件是否存在，并进行解码
				FileManager.Instance.AsyncReadJsonObject(filename, handler);
			}
			else
			{
				//检查是否存在加密数据文件，并进行解码
				filename = configData.GetEncryptSwingDataFileName();
				FileManager.Instance.AsyncReadEncryptZipJsonObject(filename, handler);
			}
		}

		public void AsyncReadBlankData(HitConfigData configData, FileReadTaskEventHandler handler)
		{
			string filename = configData.GetBlankDataFileName();
			if (File.Exists(filename))
			{
				//检查普通格式文件是否存在，并进行解码
				FileManager.Instance.AsyncReadJsonObject(filename, handler);
			}
			else
			{
				//检查是否存在加密数据文件，并进行解码
				filename = configData.GetEncryptBlankDataFileName();
				FileManager.Instance.AsyncReadEncryptZipJsonObject(filename, handler);
			}
		}

		/// <summary>
		/// 该函数创建于20150525 1546 fzl
		/// 函数主体复制于  ReadSwingClubData(string fileName)，主要功能是为了从Hd文件读取字符串数据，
		/// 然后把数据传给上传服务器
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public string ReadSwingClubDataFromFile(HitConfigData configData)
		{
			SwingClubData data = ReadSwingClubData(configData);

			if (data != null)
			{
				return Json.Serialize(data);
			}

			return null;
		}

		public bool WriteSwingClubData(SwingClubData data, HitConfigData _hitConfigData)
		{
			string encryptFile = _hitConfigData.GetEncryptSwingDataFileName();

			JObject json_data = data.Serialize(ClientConfig.DataVersionCode);

			bool result = FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

			return result;
		}

		/// <summary>
		/// 异步保存swing数据文件
		/// </summary>
		public HitConfigData AsyncSaveSwingClubData(List<HitData> hitBufferDatas, Dictionary<HitState, int> hitStates, UserInfo userInfo, string title, string desc, string max_club_angle_speed, string max_thorax_turn_angle, string max_club_head_speed, FileWriteTaskEventHandler handler)
		{
			string fileName;

			//创建swing数据文件对象
			SwingClubData _swingClubData = new SwingClubData();

			//保存hitdata数据
			_swingClubData.hitData = hitBufferDatas;

			//保存peaks数据
			_swingClubData.peakData.Clear();
			for (int s = 0; s < Enum.GetValues(typeof(HitState)).Length; s++)
			{
				if (hitStates.ContainsKey((HitState)s))
				{
					_swingClubData.peakData.Add(hitStates[(HitState)s]);
				}
				else
				{
					_swingClubData.peakData.Add(-1);
				}
			}

			//保存用户信息
			UserInfo _cloneUser = FileSave.Copy<UserInfo>(userInfo);//深度复制，该方法未经过测试，录制数据时进行测试。
			_cloneUser.bagInfo.DeleteUnengagedClubs();
			_swingClubData.userInfo = _cloneUser;

			//创建hitconfigdata
			HitConfigData _hitConfigData = new HitConfigData();
			_hitConfigData.currentSwingType =UI.Pad.OpenDataType.Swing;
			_hitConfigData.time = DateTime.Now;
			_hitConfigData.swingDataCreateTime = Utils.DateTimeToUnixTimestamp(_hitConfigData.time.ToUniversalTime()); //新加字段，记录Java格式是标准时间戳，仅用于网络同步
			_hitConfigData.userInfo = _cloneUser;

			//保存swingData 数据时，防止出现coachID 为空的现象，进行再次
			if (string.IsNullOrEmpty(_hitConfigData.userInfo.userBaseInfo.coachID)) {
				_hitConfigData.userInfo.userBaseInfo.coachID = DataManager.Instance.CurrentUser.userBaseInfo.email;
			}
			_hitConfigData.time_zone = (float)System.TimeZone.CurrentTimeZone.GetUtcOffset(System.DateTime.Now).TotalMinutes/ 60 ;
			//SwingData 描述
			_hitConfigData.swingDescTitle = title;
			_hitConfigData.swingDesc = desc;

			//保存swing数据version；
			_hitConfigData.swingVersion = _swingClubData.syncVersion;

			//保存当前使用的雷达类型， ///-----------------
			if (RadarMonitor.Instance.radarRealData != null&&RadarMonitor.Instance.isGetRealData)
			{
				_hitConfigData.radarType = RadarMonitor.Instance.radarRealData.radarType;
			}


			//异步保存swing数据文件；
			fileName = _hitConfigData.GetEncryptSwingDataFileName();
			FileManager.Instance.AsyncWriteEncryptZipJsonObject(fileName, _swingClubData.Serialize(ClientConfig.DataVersionCode), handler);

			//[{"name":"angle_speed", "value":"180" ,"unit":"deg/s" },{"name":"turn_angle", "value":"50" ,"unit":"°" },{"name":"speed", "value":"1190" ,"unit":"" }]
			//Swing 评级指标
			SwingGrade _swingGrade = new SwingGrade();
			SwingGradeObject _swingGradeObject = new SwingGradeObject("angle_speed", max_club_angle_speed, "deg/s");
			_swingGrade.gradeList.Add(_swingGradeObject);
			SwingGradeObject _swingGradeObject1 = new SwingGradeObject("turn_angle", max_thorax_turn_angle, "deg");
			_swingGrade.gradeList.Add(_swingGradeObject1);
			SwingGradeObject _swingGradeObject2 = new SwingGradeObject("speed", max_club_head_speed, "km/hr");
			_swingGrade.gradeList.Add(_swingGradeObject2);

			_hitConfigData.swingGrade = _swingGrade;

			//默认无需生成新的SwingComment,第一次使用时，创建
			SwingComment _swingComment = new SwingComment();
			_hitConfigData.swingCommentVersion = _swingComment.syncVersion;

			//异步保存标注文件
			fileName = _hitConfigData.GetEncryptCommentFileName();
			FileManager.Instance.AsyncWriteEncryptZipJsonObject(fileName, _swingComment.Serialize(ClientConfig.DataVersionCode), handler);

			//大图快照内容
			SnapShotData _snapShotData = new SnapShotData();
			_hitConfigData.snapShotVersion = _snapShotData.syncVersion;
			_hitConfigData.snapShotVersionList = _snapShotData.snapShotList.Select(f => f.syncVersion).ToList();

			//异步保存快照文件
			WriteSnapShotData(_snapShotData, _hitConfigData);
			fileName = _hitConfigData.GetEncryptSnapshotFileName();
			FileManager.Instance.AsyncWriteEncryptZipJsonObject(fileName, _snapShotData.Serialize(ClientConfig.DataVersionCode), handler);

			//异步保存swing的综述文件
			fileName = _hitConfigData.getEncryptSwingMappingFileName();
			FileManager.Instance.AsyncWriteEncryptZipJsonObject(fileName, _hitConfigData.Serialize(ClientConfig.DataVersionCode), handler);

			UpdateHitConfigData(_hitConfigData);
			return _hitConfigData;
		}


		#endregion*/



		/*public void OnSwingClubDataReaderEventStatus(string fname, FileDataType dtype, FileTaskStatus status, object data)
		{
			//检查是否当前文件
			if (currentPeolpe.CurrentHitConfigData != null)
			{
				switch (status)
				{
				case FileTaskStatus.Progressing:
					{
						break;
					}
				case FileTaskStatus.Success:
					{
						if (fname.Equals(currentPeolpe.CurrentHitConfigData.GetSwingDataFileName()) || fname.Equals(currentPeolpe.CurrentHitConfigData.GetEncryptSwingDataFileName()))
						{
							JObject jdata = data as JObject;
							int vercode = JsonHelper.GetInteger(jdata, ClientConfig.KEY_DATA_VERSION_CODE);
							SwingClubData swingClubData = new SwingClubData(jdata, vercode);

							//对旧数据整体迁移加密之后回存；
							if (vercode != ClientConfig.DataVersionCode)
							{

								fname = currentPeolpe.CurrentHitConfigData.GetSwingDataFileName();
								if (File.Exists(fname))
								{
									File.Delete(fname);
								}

								fname = currentPeolpe.CurrentHitConfigData.GetEncryptSwingDataFileName();


								FileManager.Instance.AsyncWriteEncryptZipJsonObject(fname, swingClubData.Serialize(ClientConfig.DataVersionCode), null);
							}

							ApplicationUI.Instance.closeWindowObject(WINDOWOBJECT_TYPE.PROGRESSWINDOW, SHOW_EFFECT.NONE, true);

							if (Base.It.ConfigData.enableAdjustImpactHandData)
							{
								try
								{
									swingClubData.AdjustImpactHandData();
								}
								catch (System.Exception e1)
								{
								}

							}
							//将hitdata数据下标映射成index和time
							for (int s = 0; s < swingClubData.hitData.Count; s++)
							{
								swingClubData.hitData[s].totalIndex = s;
								swingClubData.hitData[s].time = s * 1.0f / (Base.It.ConfigData.BaseFrameCountPerSecond * Base.It.ConfigData.insertFrameCount);
							}
							//foreach (var hd in swingClubData.hitData)
							//{
							//    Debug.LogErrorFormat("{0},{1},{2}"
							//        , hd.skeletonsData[(int)SkeletonType.Club].rotation.x
							//        , hd.skeletonsData[(int)SkeletonType.Club].rotation.y
							//        , hd.skeletonsData[(int)SkeletonType.Club].rotation.z);
							//}
							PlaySwingClubData(swingClubData);
							currentPeolpe.PlayerTimePoint = PlayTimePoint.LoadDataSuccess;
							MainUIContext.Instance.LastOpenHitConfigData = currentPeolpe.CurrentHitConfigData;
							//*_*在review，compare 界面，当读取一个新的数据时，需要根据当前播放数据中左右手握杆方式，进行相应的调整
							SettingControlProcess.Instance.RefreshSettingOptionByHistoryData(swingClubData.userInfo);
							//if (isSaveAndReview) //如果是从save and Review 跳转的，直接播放
							//{
							//    PeopleMag.CurrentPeolpe.SetHeadTransparentCube();
							//    PeopleMag.CurrentPeolpe.SetBodyPlanesTransform();
							//    PeopleMag.Instance.Play(0);  // 20161107 新需求，录制完成后，跳转到Review页面，新的不再进行自动播放：
							//    isSaveAndReview = false;
							//}
						}

						break;
					}
				case FileTaskStatus.Failed:
					{
						ApplicationUI.Instance.closeWindowObject(WINDOWOBJECT_TYPE.PROGRESSWINDOW, SHOW_EFFECT.NONE, true);
						PromptManager.Instance.ShowPrompt(5019, null, null, null);
						break;
					}
				}
			}
		}*/

    }
}
