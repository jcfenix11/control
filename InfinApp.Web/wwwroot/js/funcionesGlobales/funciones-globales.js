function formatearFecha(fecha) {
    if (!fecha) return '';

    return moment(fecha).format('DD/MM/YYYY HH:mm');
}