$(document).ready(function () {
    cargarTabla();
    actualizarActividadColaboradorEvent();
});

function cargarTabla() {
    let colaboradorId = null;
    let tbody = $('#tablaActividadColaborador tbody');

    tbody.empty();
    $.ajax({
        url: '/api/ActividadColaboradorApi/obtenerTodos',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data)
            if (data && data.length) {
                data.forEach(function (item) {
                    var row = `<tr>
                <td>${item.id}</td>
                <td>${item.actividad}</td>
                <td>${item.descripcion || ''}</td>
                <td>${item.estatus ? 'Activo' : 'Inactivo'}</td>
                <td>${item.rol ?? ''}</td>
                <td>${item.fechaCracion ?? ''}</td>
                <td>
                        <button class="btn btn-sm btn-warning"
                            onclick="editar(${item.id})">
                            Editar
                        </button>

                        <button class="btn btn-sm btn-danger"
                            onclick="eliminar(${item.id})">
                            Eliminar
                        </button>
                    </td>
            </tr>`;
                    tbody.append(row);
                });
            } else {
                tbody.append('<tr><td colspan="5">No hay registros</td></tr>');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error al cargar actividades:', error);
            tbody.append('<tr><td colspan="6">Error al cargar actividades</td></tr>');
        }
    }
    )
}

function formatearFecha(fecha) {
    if (!fecha) return '';
    let f = new Date(fecha);
    return f.toLocaleDateString();
}

function eliminar(id) {
    if (!confirm("¿Seguro que deseas eliminar?")) return;

    $.ajax({
        url: `/ActividadColaborador/EliminarJson/${id}`,
        type: 'DELETE',
        success: function () {
            cargarTabla();
        }
    });
}
function editar(id) {

    $.ajax({
        url: `/api/ActividadColaboradorApi/obtenerPorId/${id}`,
        type: 'GET',
        success: function (data) {

            $("#editId").val(data.id);
            $("#editActividad").val(data.actividad);
            $("#editDescripcion").val(data.descripcion);
            $("#editRol").val(data.rol);
            $("#editEstatus").val(data.estatus);
            $("#editCategoria").val(data.categoria);

            let modal = new bootstrap.Modal(document.getElementById('modalEditar'));
            modal.show();
        },
        error: function () {
            alert("Error al obtener datos.");
        }
    });

}

function actualizarActividadColaboradorEvent() {
    $("#btnGuardarEdicion").click(function () {

        let modelo = {
            id: $("#editId").val(),
            actividad: $("#editActividad").val(),
            descripcion: $("#editDescripcion").val(),
            rol: $("#editRol").val(),
            estatus: $("#editEstatus").val() === "true",
            categoria: parseInt($("#editCategoria").val())
            
        };

        $.ajax({
            url: `/api/ActividadColaboradorApi/actualizar`,
            type: 'PUT',
            contentType: "application/json",
            data: JSON.stringify(modelo),
            success: function () {

                let modalElement = document.getElementById('modalEditar');
                let modal = bootstrap.Modal.getInstance(modalElement);
                modal.hide();

                cargarTabla();
            },
            error: function () {
                alert("Error al actualizar.");
            }
        });

    });
}