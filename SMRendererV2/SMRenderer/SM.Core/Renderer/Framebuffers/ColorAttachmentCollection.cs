using System.Collections.Generic;
using System.Linq;
using SM.Core.Enums;
using SM.Core.Exceptions;

namespace SM.Core.Renderer.Framebuffers
{
    /// <summary>
    /// This contains all color attachments and make sure, that it not exceds the maximal capacity.
    /// </summary>
    public class ColorAttachmentCollection : List<ColorAttachment>
    {
        /// <summary>
        /// This returns the color attachment with the variable name.
        /// <para>Returns null, if not existing.</para>
        /// </summary>
        /// <param name="variableName">The variable its looking for.</param>
        /// <returns></returns>
        public ColorAttachment this[string variableName] => this.FirstOrDefault(a => a.FragOutputVariable == variableName);

        /// <summary>
        /// Creates a collection.
        /// </summary>
        public ColorAttachmentCollection()
        {
            Capacity = 32;
        }

        /// <summary>
        /// Adds a new color attachment.
        /// </summary>
        /// <param name="variable">The variable it should listen to.</param>
        /// <param name="scale">Sets the scale</param>
        public void Add(string variable, float scale = 1)
        {
            if (Count >= 32)
                throw new LogException("Framebuffer reached its capacity of colorAttachments.");

            int id = GetFreeIDs()[0];
            Add(new ColorAttachment(variable, id, scale));
        }

        /// <summary>
        /// Adds multiple color attachments.
        /// <para>All color attachments have the scale 1</para>
        /// </summary>
        /// <param name="variableNames">The variable names they should listen to.</param>
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

        /// <summary>
        /// Searches for every free ID.
        /// </summary>
        /// <returns>A list of free IDs</returns>
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