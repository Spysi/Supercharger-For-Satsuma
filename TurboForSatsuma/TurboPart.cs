using ModApi.Attachable;
using UnityEngine;

namespace SuperchargerForSatsuma
{
    public class Pulley : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1549.08069f, 4.7f, 736.3539f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        #endregion
        public Pulley(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {
           
            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
    public class Turbine : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1550.08289f, 4.7f, 736.396851f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        protected override void disassemble(bool startup = false)
        {
            // do stuff on dissemble.
            base.disassemble(startup); // if you want dissemble function, you need to call base!
        }
        public void removePart()
        {
            disassemble(false);
        }


        #endregion
        public Turbine(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {
            

        //rigidPart.GetComponent<MeshCollider>().enabled = false;
        // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
    }

    }
    public class Pipe : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1550.10535f, 4.7f, 736.6345f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        protected override void disassemble(bool startup = false)
        {
            // do stuff on dissemble.
            base.disassemble(startup); // if you want dissemble function, you need to call base!
        }
        public void removePart()
        {
            disassemble(false);
        }
        #endregion
        public Pipe(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {
            
            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
    public class Pipe_rac_carb : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1549.648f, 4.7f, 736.2641f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        #endregion
        public Pipe_rac_carb(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {
            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }

    public class Filter : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1550.10535f, 4.7f, 736.6345f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        protected override void disassemble(bool startup = false)
        {
            // do stuff on dissemble.
            base.disassemble(startup); // if you want dissemble function, you need to call base!
        }
        public void removePart()
        {
            disassemble(false);
        }
        #endregion
        public Filter(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {

            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }


    public class Belt : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1548.98865f, 4.7f, 736.730164f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        protected override void disassemble(bool startup = false)
        {
            // do stuff on dissemble.
            base.disassemble(startup); // if you want dissemble function, you need to call base!
        }
        public void removePart()
        {
            disassemble(false);
        }
        #endregion
        public Belt(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {

            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
    public class Turbinegauge : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1549.57312f, 4.7f, 736.6599f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        #endregion
        public Turbinegauge(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {

            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
    public class Pipe_2_carb : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1549.32129f, 4.7f, 736.4999f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        #endregion
        public Pipe_2_carb(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {

            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
    public class Switchbutton : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1550.08289f, 4.7f, 736.396851f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        #endregion
        public Switchbutton(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {
            //rigidPart.GetComponent<MeshCollider>().enabled = false;
            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
    public class HeadGasket : Part
    {
        #region Properties
        public override PartSaveInfo defaultPartSaveInfo => new PartSaveInfo() // Set default part settings here: this will be loaded if no save is found.
        {
            installed = false,
            position = new Vector3(1549.08069f, 4.7f, 736.3539f),
            rotation = Quaternion.Euler(0, 0, 0),
        };
        public override GameObject rigidPart // The installed/fixed part.
        {
            get;
            set;
        }
        public override GameObject activePart // The free/pickable part.
        {
            get;
            set;
        }
        internal PartSaveInfo getPartSaveInfo
        {
            get
            {
                return this.getSaveInfo();
            }
        }
        #endregion
        public HeadGasket(PartSaveInfo inPartSaveInfo, Trigger inPartTrigger, Vector3 inPartPosition, Quaternion inPartRotation, GameObject part, GameObject parent) : base(inPartSaveInfo, part, parent, inPartTrigger, inPartPosition, inPartRotation)
        {

            // This is where you would add components to 'RigidPart' And/Or 'ActivePart'
        }

    }
}
