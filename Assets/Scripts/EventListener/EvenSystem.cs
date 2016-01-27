using UnityEngine;
using System.Collections;

namespace Core{
//this class can be separated to several other classes
public partial class EvenSystem {

		public delegate void NoParamDelegate();
		public delegate void OneParamDelegate<T>(T t);
		//put the tower in the map
		public delegate void TargetChangeHandle(Vector3 pos, bool isTerrian);

	}
}
