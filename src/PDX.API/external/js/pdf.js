module.exports = function(callback, htmlContent, data) {
    var jsreport = require('jsreport-core')();
    var footerHtml = '<div style=" content: "";display: table;clear: both;"><div style="float: left;width: 50%;text-align:right;">{#pageNum}/{#numPages}</div><div style="float: left;width: 50%;text-align:right;font-size:10px;font-style:italic">electronically generated certificate from eRIS</div></div>';
    jsreport.options.assets = {
        // wildcard pattern for accessible linked or external files
        allowedFiles: "**/**.**",
        // enables access to files not stored as linked assets in jsreport store    
        searchOnDiskIfNotFoundInStore: true,
        // root url used when embedding assets as links {#asset foo.js @encoding=link}
        //rootUrlForLinks: "G:\\Projects\\Office\\iImport\\BackEnd\\src\\PDX.Scheduler\\assets",
        // make all assets accessible to anonymous requests
        publicAccessEnabled: true
    };
    console.log(jsreport);
    jsreport.init().then(function() {
        return jsreport.render({
            template: {
                content: htmlContent,
                phantom: {
                    orientation: "landscape",
                    footer: footerHtml,
                    zoom: 1.68
                },
                engine: 'jsrender',
                recipe: 'phantom-pdf'
            },
            data: data
        }).then(function(resp) {
            callback( /* error */ null, resp.content.toJSON().data);
        });
    }).catch(function(e) {
        callback( /* error */ e, null);
    })
};