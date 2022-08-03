using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaveData {

    void save();

    SaveData load();
}
