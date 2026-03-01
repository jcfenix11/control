$(document).ready(function () {

    $('#selectColaborador').change(function () {
        var colaboradorId = $(this).val() ;
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