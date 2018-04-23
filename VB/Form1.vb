Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
				Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i Mod 4), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
				End Function


		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			AddHandler gridView1.FocusedRowChanged, AddressOf gridView1_FocusedRowChanged
		End Sub

		Private Sub gridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As FocusedRowChangedEventArgs)
			RefreshGroupRows(e.PrevFocusedRowHandle)
			RefreshGroupRows(e.FocusedRowHandle)
		End Sub

		Private Sub RefreshGroupRows(ByVal rowHandle As Integer)
			If gridView1.IsGroupRow(rowHandle) Then
				Return
			End If
			Dim parentGroupRowHandle As Integer = gridView1.GetParentRowHandle(rowHandle)
			gridView1.RefreshRow(parentGroupRowHandle)
		End Sub
		Private Function RowContainsFocus(ByVal groupRowHandle As Integer) As Boolean
			If gridView1.IsGroupRow(groupRowHandle) Then
				For i As Integer = 0 To gridView1.GetChildRowCount(groupRowHandle) - 1
					If gridView1.GetChildRowHandle(groupRowHandle, i) = gridView1.FocusedRowHandle Then
						Return True
					End If
				Next i
			End If
			Return False
		End Function
		Private Sub gridView1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridView1.RowStyle
			Dim rowContainsFocus As Boolean = Me.RowContainsFocus(e.RowHandle)
			If rowContainsFocus Then
				e.Appearance.BackColor = Color.Yellow
			End If
		End Sub
	End Class
End Namespace