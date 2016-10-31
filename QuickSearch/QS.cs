﻿/* 
QuickSearch
Copyright 2016 Malah

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System.IO;
using System.Reflection;
using UnityEngine;

namespace QuickSearch {
	[KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
	public partial class QRnD : QuickSearch { }

	[KSPAddon(KSPAddon.Startup.EditorAny, false)]
	public partial class QEditor : QuickSearch { }

	public partial class QuickSearch : MonoBehaviour {

		public readonly static string VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public readonly static string MOD = Assembly.GetExecutingAssembly().GetName().Name;
		public readonly static string PATH = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location) + "/..";
		public static string relativePath {
			get {
				string gData = "/GameData/";
				string plugins = "/Plugins/";
				int gDataIndex = PATH.LastIndexOf (gData, System.StringComparison.Ordinal) + gData.Length;
				int pluginsIndex = PATH.LastIndexOf (plugins, System.StringComparison.Ordinal);
				return PATH.Substring (gDataIndex, pluginsIndex - gDataIndex);
			}
		}

		[KSPField(isPersistant = true)] static QBlizzyToolbar BlizzyToolbar;

		protected static void Log(string String, string Title = null) {
			if (Title == null) {
				Title = MOD;
			} else {
				Title = string.Format ("{0}({1})", MOD, Title);
			}
			if (QSettings.Instance.Debug) {
				Debug.Log (string.Format ("{0}[{1}]: {2}", Title, VERSION, String));
			}
		}
		protected static void Warning(string String, string Title = null) {
			if (Title == null) {
				Title = MOD;
			} else {
				Title = string.Format ("{0}({1})", MOD, Title);
			}
			Debug.LogWarning (string.Format ("{0}[{1}]: {2}", Title, VERSION, String));
		}

		protected virtual void Awake() {
			if (BlizzyToolbar == null) BlizzyToolbar = new QBlizzyToolbar ();
			TextField = new GUIStyle(HighLogic.Skin.textField);
			TextField.stretchWidth = true;
			TextField.stretchHeight = true;
			TextField.alignment = TextAnchor.MiddleCenter;
			Log ("Awake");
		}
		protected virtual void Start() {
			if (BlizzyToolbar != null) BlizzyToolbar.Init ();
			Log ("Start");
		}
		protected virtual void OnDestroy() {
			if (BlizzyToolbar != null) BlizzyToolbar.Destroy ();
			Log ("OnDestroy");
		}
	}
}