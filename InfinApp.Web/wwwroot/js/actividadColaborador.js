$(document).ready(function () {
    cargarTabla();
    actualizarActividadColaboradorEvent();
    abrirModalCrear();
    crearActividadEvent();
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
                <td>${formatearFecha(item.fechaCracion) ?? ''}</td>
                <td>
                        <button class="btn btn-sm btn-warning"
                            onclick="editar(${item.id})">
                            Editar
                        </button>

                        <button class="btn btn-sm btn-danger btnEliminar" onclick="eliminar(${item.id})">
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

function abrirModalCrear() {

    $("#btnNuevaActividad").click(function () {

        $("#formCrearActividad")[0].reset();

        let modal = new bootstrap.Modal(
            document.getElementById("modalCrearActividad")
        );

        modal.show();

    });

}

function crearActividadEvent() {

    $("#btnGuardarNuevaActividad").click(function () {

        let modelo = {

            actividad: $("#crearActividad").val(),
            descripcion: $("#crearDescripcion").val(),
            rol: parseInt($("#crearRol").val()),
            categoria: parseInt($("#crearCategoria").val()),
            estatus: $("#crearEstatus").val() === "true",
            fechaCracion: new Date()

        };

        $.ajax({

            url: '/api/ActividadColaboradorApi/crear',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(modelo),

            success: function () {

                Swal.fire({
                    icon: 'success',
                    title: 'Actividad creada correctamente',
                    timer: 1500,
                    showConfirmButton: false
                });

                let modal = bootstrap.Modal.getInstance(
                    document.getElementById("modalCrearActividad")
                );

                modal.hide();

                cargarTabla();

            },

            error: function (xhr) {
                conosole.log('error al crear actividad: ', xhr.responseText);
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se pudo crear la actividad'
                });
            }
        });
    });
}

function eliminar(id) {

        Swal.fire({
            title: "¿Estás seguro?",
            text: "Esta actividad será eliminada.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#6c757d",
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then((result) => {

            if (result.isConfirmed) {

                $.ajax({
                    url: `/api/ActividadColaboradorApi/${id}`,
                    type: "DELETE",

                    success: function () {

                        Swal.fire({
                            icon: "success",
                            title: "Eliminado",
                            text: "La actividad fue eliminada correctamente",
                            timer: 1500,
                            showConfirmButton: false
                        });

                        cargarTabla();
                    },

                    error: function () {

                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: "No se pudo eliminar la actividad"
                        });

                    }
                });

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
    $("#btnGuardarEdicion").off("click").on("click", function () {

        let modelo = {
            id: parseInt($("#editId").val()),
            actividad: $("#editActividad").val(),
            descripcion: $("#editDescripcion").val(),
            rol: parseInt($("#editRol").val()),
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

                Swal.fire({
                    icon: 'success',
                    title: 'Se actualizó correctamente'
                });

                console.log(xhr.responseText);
            },
            error: function (xhr) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error al actualizar',
                    text: 'No se pudo actualizar la actividad'
                });

                console.log(xhr.responseText);
            }
        });

    });
}