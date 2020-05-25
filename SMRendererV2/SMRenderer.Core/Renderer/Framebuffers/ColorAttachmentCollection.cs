using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;

namespace SMRenderer.Core.Renderer.Framebuffers
{
    public class ColorAttachmentCollection : List<ColorAttachment>
    {
        public ColorAttachment this[string variableName] => this.FirstOrDefault(a => a.FragOutputVariable == variableName);

        public ColorAttachmentCollection()
        {
            Capacity = 32;
        }

        public void Add(params string[] variableNames)
        {
            if (Count >= 32)
                throw new LogException("Framebuffer reached its capacity of colorAttachments.");
            
            List<int> freeIds = new List<int>();
            for (int i = 0; i < 32; i++)
            {
                if (this.Any(a => a.AttachmentID != i) || Count == 0)
                {
                    freeIds.Add(i);

                    if (freeIds.Count >= variableNames.Length) break;
                }
            }

            string leftVariables = "";
            foreach (string variableName in variableNames)
            {
                if (freeIds.Count <= 0)
                {
                    leftVariables += variableName;
                    continue;
                }

                Add(new ColorAttachment(variableName, freeIds[0]));
                freeIds.Remove(freeIds[0]);
            }

            if (leftVariables != "")
                Log.Write(LogWriteType.Warning, "Following ColorAttachments exceds the capacity of possible color attachments: " + leftVariables);
        }
    }
}