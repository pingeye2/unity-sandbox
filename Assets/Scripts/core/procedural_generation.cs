using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class procedural_generation : MonoBehaviour {

    private zone[] zones;
    private GameObject[] zone_blocks;

    // constructor -> takes the arr of GameObjects and assigns them to zones
    public procedural_generation(GameObject[] blocks) {
        int count = 0;
        int zone_count = 0;
        for(int i = 0; i < blocks.Length; i++) {
            zone_blocks[count] = blocks[i];
            if(count == 20) {
                zones[zone_count] = new zone(zone_blocks);
                zone_count++;
                Array.Clear(zone_blocks, 0, zone_blocks.Length);                
                count = 0;
            }
        }
    }

} 

public class zone : MonoBehaviour {

    public zone(GameObject[] zone_blocks) {
       create_zone(zone_blocks);
    }

    private GameObject[] create_zone(GameObject[] arr) {
        return arr;
    }

}