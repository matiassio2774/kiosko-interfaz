Public Class Form1

    Private Sub actualizarPantalla()
        Me.GolosinasTableAdapter.Fill(Me.KioscoDataSet.golosinas)
    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles BtnInsertar.Click
        Dim sql As String
        Dim conex As New SQLConnect
        Try
            'Consulta SQL para insertar registros en la tabla golosinas
            sql = "use kiosco; insert into golosinas values('" + txtid.Text + "', '" + txtname.Text + "', '" + txtCantidad.Text + "', '" + txtPrecio.Text + "');"
            conex.EjecutarSQL(sql)
            conex.Dispose()
            MsgBox("Registro insertado correctamente")

        Catch ex As Exception
            MsgBox("Error al insertar registro")
        End Try
    End Sub

    Private Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click
        Me.GolosinasTableAdapter.Fill(Me.KioscoDataSet.golosinas)
        Dim sql As String
        Dim conex As New SQLConnect
        'Consulta SQL para buscar una golosina en la tabla
        Try
            sql = "USE kiosco; SELECT * FROM kiosco.golosinas WHERE id LIKE('" + txtid.Text + "') OR nombre LIKE('" + txtname.Text + "');"
            conex.EjecutarSQL(sql)

            conex.DataAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(conex.miComando)
            conex.DataAdapter.Fill(conex.DataSet)
            DataGridView1.DataSource = conex.DataSet.Tables(0).DefaultView

            conex.Dispose()
            MsgBox("Consulta ejecutada correctamente.")
            actualizarPantalla()
        Catch ex As Exception
            MsgBox("Error al realizar la busqueda.")
        End Try


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim sql As String
        Dim conex As New SQLConnect
        Try
            'Consulta SQL para tirar select a todos los registros de la tabla golosinas
            sql = "USE kiosco; SELECT * FROM kiosco.golosinas;"
            conex.EjecutarSQL(sql)
            conex.Dispose()
            MsgBox("Acceso permitido")

        Catch ex As Exception
            'Crea el schema kiosko y la tabla con sus columnas
            sql = " Create schema kiosco; use kiosco; Create table golosinas (id tinyint Primary Key, nombre varchar(20), cantidad int, precio float);"
            conex.EjecutarSQL(sql)
            conex.Dispose()
            MsgBox("Tabla creada correctamente!")
        End Try
        Me.GolosinasTableAdapter.Fill(Me.KioscoDataSet.golosinas)
        Me.DataGridView1.ReadOnly = True ' Se hace el DGV para solo lectura
    End Sub

    Private Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim sql As String
        Dim conex As New SQLConnect


        Try
            'Consulta para updatear las golosinas
            sql = "USE kiosco; UPDATE golosinas SET nombre = '" + txtname.Text + "', cantidad= '" + txtCantidad.Text + "' ,precio='  " + txtPrecio.Text + "' WHERE id=(' " + txtid.Text + "');"

            conex.EjecutarSQL(sql)
            conex.Dispose()

            MsgBox("Registro modificado correctamente.")
            actualizarPantalla()

        Catch ex As Exception

            MsgBox("Error al realizar la mofidicacion")

        End Try
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim sql As String
        Dim conex As New SQLConnect


        Try
            'Consulta para dropear un registro.
            sql = "USE kiosco; DELETE FROM golosinas WHERE id=(' " + txtid.Text + "');"

            conex.EjecutarSQL(sql)
            conex.Dispose()

            MsgBox("Registro eliminado correctamente.")

        Catch ex As Exception

            MsgBox("Error al eliminar el registro.")

        End Try
    End Sub

    Private Sub BtnModPrecio_Click(sender As Object, e As EventArgs) Handles BtnModPrecio.Click
        Dim sql As String
        Dim conex As New SQLConnect


        If RBSubir.Checked = True Then
            sql = "USE kiosco; UPDATE golosinas SET precio = precio + " + TxtModPrecio.Text + " WHERE id = " + TxtId2.Text + ";"
            conex.EjecutarSQL(Sql)
            conex.Dispose()

            MsgBox("Precio modificado correctamente.")
            actualizarPantalla()

        ElseIf RBBajar.Checked Then
            sql = "USE kiosco; UPDATE golosinas SET precio = precio - " + TxtModPrecio.Text + " WHERE id = " + TxtId2.Text + ";"

            conex.EjecutarSQL(Sql)
            conex.Dispose()

            MsgBox("Precio modificado correctamente.")
            actualizarPantalla()

        End If
    End Sub
End Class
