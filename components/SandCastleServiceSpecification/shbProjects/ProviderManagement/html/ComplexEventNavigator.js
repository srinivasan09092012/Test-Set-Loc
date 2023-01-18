
//object blueprint to hold page display name and url						 
var breadCrumbsContentDefaultItem = {
								DisplayName:"Main Event Attributes", 
								TargetUrl:"http://localhost:8080/ProviderManagement/APISeviceSpecification/html/T_HP_HSP_UA3_ProviderManagement_BAS_Providers_Contracts_Events_ServiceLocationAdded.htm",
								IsActive: false
							 };

// starting the website breadCrumb display event main page only
// making all generic
var breadCrumbsContent = [];

//UI breadCrum id 
var breadCrumbsName = "BreadCrumbsEventPage";

//UI div to update with ajax.load
var mainDiv = "#ID4RBSection";

//removeBreadCrumbs
//usage:  remove item from array and call the update breadscrumb UI control
function removeBreadCrumbs(index) {
	index++;
    breadCrumbsContent.splice(index, ((breadCrumbsContent.length - 1)-index == 0)?1:(breadCrumbsContent.length - 1)-index);
	breadCrumbsContent[breadCrumbsContent.length - 1].IsActive = true;
	document.getElementById(breadCrumbsName).innerHTML = '';
    breadCrumbsContent.forEach(renderBreadCrumbItem);
}

//InitializeBreadCrumbs
//params: Object from the documentReady method 
//usage: To initialize the breadscrumb first element 
function InitializeBreadCrumbs(breadCrumbsContentDefaultItemFromSite)
{
	breadCrumbsContent.push(breadCrumbsContentDefaultItemFromSite);
}

//renderBreadCrumbItem
//params: Object that holds DisplayName, TargetUrl, IsActive
//usage:  iterate the array and render the updated breadCrumb
function renderBreadCrumbItem(value, index, array) {
	var node = document.createElement("LI");  
	node.classList.add("breadcrumb-item");
	var listItemLabel = document.createTextNode(value.DisplayName);         
	var listItemwithValue = document.createElement('a');
	
	if(value.IsActive)
	{
		node.classList.add("active");

		if(breadCrumbsContent.length == 1)
		{
			location.replace(breadCrumbsContent[0].TargetUrl);
		}
	}
	else
	{
		listItemwithValue.setAttribute('href', '#!');
		listItemwithValue.setAttribute('onclick','javascript: removeBreadCrumbs('+ index +');');
		
	}
	
	listItemwithValue.appendChild(listItemLabel);
    node.appendChild(listItemwithValue);  
	document.getElementById(breadCrumbsName).appendChild(node);
	
	loadHtml(breadCrumbsContent[breadCrumbsContent.length - 1].TargetUrl);
}

//updateBreadCrumb
//params: Object that holds DisplayName, TargetUrl, IsActive
//usage:  Entry point from event page
function updateBreadCrumb(newItem){
	breadCrumbsContent[breadCrumbsContent.length - 1].IsActive = false;
	breadCrumbsContent.push(newItem);
	document.getElementById(breadCrumbsName).innerHTML = '';
	breadCrumbsContent.forEach(renderBreadCrumbItem);

	//loadHtml(newItem.TargetUrl);
	//breadCrumbsContent.forEach(printArray);
	//debug and trace for coding.
	//breadCrumbsContent.forEach(printArray);
	//alert(document.getElementById(breadCrumbsName).innerHTML);
}

function printArray(value, index, array)
{
	alert(value.DisplayName + " " + value.TargetUrl + " " + value.IsActive)
}

function loadHtml(path){
	$(mainDiv).empty();
	$(mainDiv).load(path);
}



