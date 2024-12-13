using System.Diagnostics;

namespace DiskSpaceAndUserManagement
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <remarks>
        /// Add your cleanup code here if necessary.
        /// </remarks>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}