using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TemplateSlotButton : MonoBehaviour
{
    void Start()
    {
        UpdateName();
    }

    void UpdateName()
    {
        /*TextAsset textAsset = (TextAsset)Resources.Load("Templates/"+gameObject.name);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        BuildingSerializer.ins.DeserializeBuilding();*/
    }
}
