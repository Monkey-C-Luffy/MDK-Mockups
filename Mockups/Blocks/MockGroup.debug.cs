using System;
using VRage.Game;
using Sandbox.ModAPI.Ingame;
using VRage.ObjectBuilders;
using VRageMath;
using System.Collections.Generic;

namespace IngameScript.Mockups.Blocks
{
#if !MOCKUP_DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class MockGroup : IMyBlockGroup
    {
        private string _name;
        List<IMyTerminalBlock> _cubeBlocks = new List<IMyTerminalBlock>();
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public MockGroup(string Name,List<IMyTerminalBlock> mockCubeBlocks)
        {
            _name = Name;
            _cubeBlocks = mockCubeBlocks;
        }
        public void GetBlocks(List<IMyTerminalBlock> blocks,Func<IMyTerminalBlock,bool> collect = null)
        {
            blocks.Clear();
            if (collect == null)
            {
                blocks.AddList(_cubeBlocks);
                return;
            }
            foreach (IMyTerminalBlock block in _cubeBlocks)
            {
                if(collect.Invoke(block)) blocks.Add(block);
            }
        }

        public void GetBlocksOfType<T>(List<IMyTerminalBlock> blocks,Func<IMyTerminalBlock,bool> collect = null) where T : class
        {
            blocks.Clear();
            foreach (IMyTerminalBlock block in _cubeBlocks)
            {
                if(typeof(T).IsAssignableFrom(block.GetType()))
                {
                    if (collect == null) blocks.Add(block);
                    else if(collect.Invoke(block)) blocks.Add(block);
                }
            };
        }

        public void GetBlocksOfType<T>(List<T> blocks,Func<T,bool> collect = null) where T : class
        {
            blocks.Clear();
            foreach (IMyTerminalBlock block in _cubeBlocks)
            {
                if (typeof(T).IsAssignableFrom(block.GetType()))
                {
                    T tempBlock = block as T;
                    if (collect == null) blocks.Add(tempBlock);
                    else if (collect.Invoke(tempBlock)) blocks.Add(tempBlock);
                }
            }
        }
    }
}
