$('.daterange').daterangepicker({
    autoUpdateInput: false,
    singleDatePicker: true,
    autoApply: true,
    locale: {
        "format": "DD/MM/YYYY",
        "separator": " - ",
        "applyLabel": "Aplicar",
        "cancelLabel": "Cancelar",
        "daysOfWeek": [
            "Dom",
            "Seg",
            "Ter",
            "Qua",
            "Qui",
            "Sex",
            "Sab"
        ],
        monthNames: [
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro"
        ],
        firstDay: 1
    }
});

$('.daterange').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('DD/MM/YYYY'));
});

$('.daterange').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
    $(this).closest('.row').find('.daterange').val('');
});
