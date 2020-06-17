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

        public ColorAttachmentCollection(float scaling = 1)
        {
            Capacity = 32;
        }

        public void Add(string variable, float scale = 1)
        {
            if (Count >= 32)
                throw new LogException("Framebuffer reached its capacity of colorAttachments.");

            int id = GetFreeIDs()[0];
            Add(new ColorAttachment(variable, id, scale));
        }

        public void Add(params string[] variableNames)
        {
            if (Count >= 32)
                throw new LogException("Framebuffer reached its capacity of colorAttachments.");

            List<int> freeIds = GetFreeIDs();

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

        public List<int> GetFreeIDs()
        {
            List<int> freeIds = new List<int>();
            for (int i = 0; i < 32; i++)
            {
                if (this.Any(a => a.AttachmentID != i) || Count == 0)
                {
                    freeIds.Add(i);
                }
            }

            return freeIds;
        }
    }
}