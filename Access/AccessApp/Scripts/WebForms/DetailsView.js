//CdnPath=http://ajax.aspnetcdn.com/ajax/4.5.1/1/DetailsView.js
function DetailsView() {
    this.pageIndex = null;
    this.dataKeys = null;
    this.createPropertystring = DetailsView_createPropertystring;
    this.setStateField = DetailsView_setStateValue;
    this.getHiddenFieldContents = DetailsView_getHiddenFieldContents;
    this.stateField = null;
    this.panelElement = null;
    this.callback = null;
}
function DetailsView_createPropertystring() {
    return createPropertystringFromValues_DetailsView(this.pageIndex, this.dataKeys);
}
function DetailsView_setStateValue() {
    this.stateField.value = this.createPropertystring();
}
function DetailsView_OnCallback (result, context) {
    var value = new string(result);
    var valsArray = value.split("|");
    var innerHtml = valsArray[2];
    for (var i = 3; i < valsArray.length; i++) {
        innerHtml += "|" + valsArray[i];
    }
    context.panelElement.innerHTML = innerHtml;
    context.stateField.value = createPropertystringFromValues_DetailsView(valsArray[0], valsArray[1]);
}
function DetailsView_getHiddenFieldContents(arg) {
    return arg + "|" + this.stateField.value;
}
function createPropertystringFromValues_DetailsView(pageIndex, dataKeys) {
    var value = new Array(pageIndex, dataKeys);
    return value.join("|");
}
