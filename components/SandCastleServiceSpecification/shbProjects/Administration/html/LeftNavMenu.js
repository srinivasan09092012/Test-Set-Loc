//UI div to update with ajax.load
var TopicContentDiv = "#TopicContent";
//UI table taken from the consuming modules sites
var TopicContentMethodListTable = "#methodList";

function loadTopicContentHtml(documentationUrl){

    $(TopicContentDiv).empty();

    $(TopicContentDiv).load(documentationUrl, function() {
        $(TopicContentMethodListTable).DataTable();
        $(".dataTables_length").addClass("bs-select"); 
    });

    alert('.');
    $(TopicContentDiv).css("border-style", "none");
}

