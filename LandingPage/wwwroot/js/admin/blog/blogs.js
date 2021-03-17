$(document).ready(function () {
    // Config jquery tab 
    $("#tabs").tabs();
    // Config datatable
    $('#blog-table').DataTable();
    // Config tiny CME
    tinymce.init({ selector: '#content' });
    // Config Chosen
    $(".category").chosen();
})