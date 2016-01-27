using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
//using a pool to manage the objects, the perfabs
public class MemoryManager : UnityAllSceneSingleton<MemoryManager>, IMessageObject {

	List<object> structs = new List<object>();
	Dictionary<string, List<object>> structLists = new Dictionary<string, List<object>>();

	public object CreateNativeStruct(string className){
		this.START_METHOD ("CreateNativeStruct");
		Type type = Type.GetType (className);
		//only allow 100 game object been created
		if (structs.Count <= 100) {
			if(!structLists.ContainsKey(className)){
				structLists.Add(className,structs);
			}
			else {
				structs = structLists[className];
			}
			object obj = Activator.CreateInstance(Type.GetType(className));
			structs.Add(obj);
			this.END_METHOD("CreateNativeStruct");
			return obj;
		}
		//if the game object is more than 100, throw the exception
		throw new UnityException("try to create wrong struct");
	}

	public override void Awake(){
		base.Awake ();
		//InvokeRepeating("ResizeDic",120,300);
	}
}
