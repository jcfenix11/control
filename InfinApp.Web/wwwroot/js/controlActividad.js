let fechaInicioPicker;
let fechaFinPicker;

document.addEventListener("DOMContentLoaded", function () {
    inicializarCalendarsEvent();
});

$(document).ready(function () {

    $('#selectColaborador').change(function () {
        var colaboradorId = $(this).val();
        var tbody = $('#tablaActividades tbody');
        tbody.empty();

        if (!colaboradorId) return;

        $.ajax({
            url: '/api/ControlActividadApi/PorColaborador/' + colaboradorId,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                if (data && data.length) {
                    data.forEach(function (item) {
                        var row = `<tr>
                            <td>${item.id}</td>
                            <td>${item.actividadNombre}</td>
                            <td>${item.descripcion || ''}</td>
                            <td>${item.fechaInicio ? new Date(item.fechaInicio).toLocaleString() : ''}</td>
                            <td>${item.fechaFin ? new Date(item.fechaFin).toLocaleString() : ''}</td>
                            <td>${item.corresponde}</td>
                        </tr>`;
                        tbody.append(row);
                    });
                } else {
                    tbody.append('<tr><td colspan="6">No hay actividades para este colaborador</td></tr>');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error al cargar actividades:', error);
                tbody.append('<tr><td colspan="6">Error al cargar actividades</td></tr>');
            }
        });
    });

});

function inicializarCalendarsEvent() {
    fechaInicioPicker = new tempusDominus.TempusDominus(
        document.getElementById("fechaInicioPicker"),
        {
            display: {
                components: {
                    calendar: true,
                    date: true,
                    month: true,
                    year: true,
                    decades: true,
                    clock: true,
                    hours: true,
                    minutes: true,
                    seconds: false
                }
            }
        }
    );

    fechaFinPicker = new tempusDominus.TempusDominus(
        document.getElementById("fechaFinPicker"),
        {
            display: {
                components: {
                    calendar: true,
                    clock: true
                }
            }
        }
    );

}


$("#btnGuardarControlActividad").click(function () {

    let modelo = {
        cacFechaInicio: fechaInicioPicker.dates.lastPicked.toISOString(),
        cacFechaFin: fechaFinPicker.dates.lastPicked.toISOString(),
        cacCorresponde: $("#createCorresponde").val() === "true",
        cacAclId: parseInt($("#createActividad").val())
    };

    $.ajax({
        url: "/api/ControlActividadApi/crear",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(modelo),

        success: function () {

            Swal.fire({
                icon: "success",
                title: "Actividad creada",
                timer: 1500,
                showConfirmButton: false
            });

            bootstrap.Modal.getInstance(
                document.getElementById("modalCrearActividad")
            ).hide();

        },

        error: function () {

            Swal.fire({
                icon: "error",
                title: "Error al crear actividad"
            });

        }

    });

});