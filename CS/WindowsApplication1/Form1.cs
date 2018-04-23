using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
                private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i % 4), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }


        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            gridView1.FocusedRowChanged += new FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
        }

        void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            RefreshGroupRows(e.PrevFocusedRowHandle);
            RefreshGroupRows(e.FocusedRowHandle);
        }

        private void RefreshGroupRows(int rowHandle)
        {
            if (gridView1.IsGroupRow(rowHandle))
                return;
            int parentGroupRowHandle = gridView1.GetParentRowHandle(rowHandle);
            gridView1.RefreshRow(parentGroupRowHandle);
        }
        private bool RowContainsFocus(int groupRowHandle)
        {
            if (gridView1.IsGroupRow(groupRowHandle))
            {
                for (int i = 0; i < gridView1.GetChildRowCount(groupRowHandle); i++)
                {
                    if (gridView1.GetChildRowHandle(groupRowHandle, i) == gridView1.FocusedRowHandle)
                        return true;
                }
            }
            return false;
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            bool rowContainsFocus = RowContainsFocus(e.RowHandle);
            if (rowContainsFocus)
                e.Appearance.BackColor = Color.Yellow;
        }
    }
}