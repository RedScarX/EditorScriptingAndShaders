using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoEditor
{
    
    [System.Serializable]
    public class Weapon
    {
        public int weaponID;
        public string weaponName;
        public string weaponType;
    }

    public class Player : MonoBehaviour
    {
        public int id;
        public new string name;
        public string description;
        public float damage;
        public float health;

        public  Weapon[] weapon = new Weapon[6];

        private void OnCollisionEnter(Collision collision)
        {
            
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            throw new NotImplementedException();
        }

        private void OnCollisionExit(Collision other)
        {
            throw new NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            throw new NotImplementedException();
        }
    }
}
