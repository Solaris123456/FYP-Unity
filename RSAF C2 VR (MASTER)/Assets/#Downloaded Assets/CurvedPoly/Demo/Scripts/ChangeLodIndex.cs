using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLab.CurvedPoly;



namespace MLab.CurvedPoly.Demo {

    /* This is a very simple script which allows you to increase 
     or decrease the LoD on on any CurvedPoly. 
     This works in play or in execution mode.
     Take it as an example about how to control CurvedPoly LoDs in your scripts.*/
    public class ChangeLodIndex : MonoBehaviour {

        /* An assigned list of Curved Poly you want this script to work on */
        public CurvedPoly[] curvedPolys;

        /* Decrease the LoDIndex of assigned CurvedPolys. 
         This, of course, will increase the tesellation of the model
         only if its LoDs asset order its LoDs from the most detailed to the less detailed.
         This is what it happens if you are using the default LoDs file.*/
        public void increaseLoD()
        {
            

            //Do this for all curvedPolys assigned to the script.
            foreach (CurvedPoly curvedPoly in curvedPolys) {

                //Check that the lods and curvedpoly assets are not null. 
                if (curvedPoly.lods != null && curvedPoly.curvedPoly != null) {

                    //Look for the lodsCount, the number of available lods in the LoDs asset
                    LoDs lods = curvedPoly.lods;
                    int lodsCount = lods.availableLoqs.Count;

                    //Decrease the lod Index
                    int lodIndex = curvedPoly.LodIndex;
                    int newLodIndex = lodIndex - 1;

                    //If the new lodIndex is a good index for one of the available lods in the LoDs asset
                    if (lodsCount > 0 && newLodIndex > 0) {

                        //Send a kind message to the console.
                        Debug.Log("Increasing LoD for "+curvedPoly.curvedPoly+" to LodIndex "+newLodIndex);

                        /*Change the LodIndex. Changes will be applied the next time 
                         the Update function of the curvedPoly is called*/
                        curvedPoly.LodIndex = newLodIndex;
                    }

                }

            }
        }

        /* Increase the LoDIndex of assigned CurvedPolys. 
         This, of course, will decrease the tesellation of the model
         only if its LoDs asset order its LoDs from the most detailed to the less detailed.
         This is what it happens if you are using the default LoDs file.*/
        public void decreaseLoD()
        {
            //Do this for all curvedPolys assigned to the script.
            foreach (CurvedPoly curvedPoly in curvedPolys)
            { 
                Debug.Log("Decreasing LoD for " + curvedPoly.curvedPoly + " Where " + (curvedPoly.lods==null)+" "+(curvedPoly.curvedPoly == null));
                if (curvedPoly.lods != null && curvedPoly.curvedPoly != null)
                {

                    //Look for the lodsCount, the number of available lods in the LoDs asset
                    LoDs lods = curvedPoly.lods;
                    int lodsCount = lods.availableLoqs.Count;

                    //Increase the lod Index
                    int lodIndex = curvedPoly.LodIndex;
                    int newLodIndex = lodIndex + 1;

                    //If the new lodIndex is a good index for one of the available lods in the LoDs asset
                    if (lodsCount > 0 && newLodIndex < lodsCount)
                    {
                        //Send a kind message to the console.
                        Debug.Log("Decreasing LoD for " + curvedPoly.curvedPoly + " to LodIndex " + newLodIndex);

                        /*Change the LodIndex. Changes will be applied the next time 
                         the Update function of the curvedPoly is called*/
                        curvedPoly.LodIndex = newLodIndex;
                    }

                }

            }
        }
    }
}
