using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot ("dialogues")]
public class Dialogues {
    [XmlElement ("dialogue")]
    public List<Dialogue> dialogues;
}