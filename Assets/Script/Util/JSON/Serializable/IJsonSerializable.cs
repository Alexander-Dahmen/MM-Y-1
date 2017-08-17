using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json {
	public interface IJsonSerializable {
		JsonObject toJsonObject();
	}
}
