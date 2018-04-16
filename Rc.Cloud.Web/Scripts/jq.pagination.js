/**
* This jQuery plugin displays pagination links inside the selected elements.
*
* @author Gabriel Birke (birke *at* d-scribe *dot* de)
* @version 1.2
* @param {int} maxentries Number of entries to paginate
* @param {Object} opts Several options (see README for documentation)
* @return {Object} jQuery Object
*/
jQuery.fn.pagination = function (maxentries, opts) {
    opts = jQuery.extend({
        items_per_page: 10, //每页显示的条目数
        num_display_entries: 4, //连续分页主体部分显示的分页条目数
        current_page: 0, //当前选中的页面 可选参数，默认是0，表示第1页
        num_edge_entries: 2, //两侧显示的首尾分页的条目数
        link_to: "javascript:;", //分页的链接
        prev_text: "上一页", //“前一页”分页按钮上显示的文字
        next_text: "下一页", //“下一页”分页按钮上显示的文字
        ellipse_text: "...", //省略的页数用什么文字表示
        prev_show_always: true, //是否显示“前一页”分页按钮
        next_show_always: true, //是否显示“下一页”分页按钮
        callback: function () { return false; }
    }, opts || {});

    return this.each(function () {
        /**
        * Calculate the maximum number of pages
        */
        function numPages() {
            return Math.ceil(maxentries / opts.items_per_page);
        }

        /**
        * Calculate start and end point of pagination links depending on 
        * current_page and num_display_entries.
        * @return {Array}
        */
        function getInterval() {
            var ne_half = Math.ceil(opts.num_display_entries / 2);
            var np = numPages();
            var upper_limit = np - opts.num_display_entries;
            var start = current_page > ne_half ? Math.max(Math.min(current_page - ne_half, upper_limit), 0) : 0;
            var end = current_page > ne_half ? Math.min(current_page + ne_half, np) : Math.min(opts.num_display_entries, np);

            return [start, end];
        }

        /**
        * This is the event handling function for the pagination links. 
        * @param {int} page_id The new page number
        */
        function pageSelected(page_id, evt) {
            current_page = page_id;
            drawLinks();
            var continuePropagation = opts.callback(page_id, panel);
            if (!continuePropagation) {
                if (evt.stopPropagation) {
                    evt.stopPropagation();
                }
                else {
                    evt.cancelBubble = true;
                }
            }
            return continuePropagation;
        }

        /**
        * This function inserts the pagination links into the container element
        */
        function drawLinks() {
            panel.empty();
            var interval = getInterval();
            var np = numPages();
            // This helper function returns a handler function that calls pageSelected with the right page_id
            var getClickHandler = function (page_id) {
                return function (evt) { return pageSelected(page_id, evt); }
            }
            // Helper function for generating a single link (or a span tag if it's the current page)
            var appendItem = function (page_id, appendopts) {
                page_id = page_id < 0 ? 0 : (page_id < np ? page_id : np - 1); // Normalize page id to sane value
                appendopts = jQuery.extend({ text: page_id + 1, classes: "" }, appendopts || {});
                if (page_id == current_page) {
                    var lnk = jQuery("<li class='active current'><a href='javascript:void(0);'>" + (appendopts.text) + "</a></li>");
                }
                else {
                    var lnk = jQuery("<li><a href='" + opts.link_to.replace(/__id__/, page_id) + "'>" + (appendopts.text) + "</a></li>")
						.bind("click", getClickHandler(page_id));
                    //.attr('href', opts.link_to.replace(/__id__/, page_id));


                }
                if (appendopts.classes) { lnk.addClass(appendopts.classes); }
                panel.append(lnk);
            }

            // Generate "Home"-Link
            appendItem(0, { text: "首页", classes: "home" });

            // Generate "Previous"-Link
            if (opts.prev_text && (current_page > 0 || opts.prev_show_always)) {
                appendItem(current_page - 1, { text: opts.prev_text, classes: "prev" });
            }
            // Generate starting points
            if (interval[0] > 0 && opts.num_edge_entries > 0) {
                var end = Math.min(opts.num_edge_entries, interval[0]);
                for (var i = 0; i < end; i++) {
                    appendItem(i);
                }
                if (opts.num_edge_entries < interval[0] && opts.ellipse_text) {
                    jQuery("<li><a href='javascript:void(0);'>" + opts.ellipse_text + "</a></li>").appendTo(panel).bind("click", getClickHandler(interval[0] - 1));
                }
            }
            // Generate interval links
            for (var i = interval[0]; i < interval[1]; i++) {
                appendItem(i);
            }
            // Generate ending points
            if (interval[1] < np && opts.num_edge_entries > 0) {
                if (np - opts.num_edge_entries > interval[1] && opts.ellipse_text) {
                    jQuery("<li><a href='javascript:void(0);'>" + opts.ellipse_text + "</a></li>").appendTo(panel).bind("click", getClickHandler(interval[1]));
                }
                var begin = Math.max(np - opts.num_edge_entries, interval[1]);
                for (var i = begin; i < np; i++) {
                    appendItem(i);
                }

            }
            // Generate "Next"-Link
            if (opts.next_text && (current_page < np - 1 || opts.next_show_always)) {
                appendItem(current_page + 1, { text: opts.next_text, classes: "next" });
            }

            // Generate "Last"-Link
            appendItem(np - 1, { text: "尾页", classes: "home" });

            // Generate toatal record count and pagecount
            //appendItem(current_page, { text: "共" + maxentries + "条", classes: "home" });
            appendItem(current_page, { text: "页码:" + (current_page + 1) + "/" + np, classes: "home" });
            var arrPage = [5, 10, 12, 15, 20, 30, 50, 75, 100];
            // Generate toatal record count and pagecount
            var pageSelect = "<select data-name=\"pagination_select\" name=\"pagination_select\" class=\"pagination_select\" onchange=\"pageselectCallback(0);\">";
            for (var i = 0; i < arrPage.length; i++) {
                pageSelect += "<option value=\"" + arrPage[i] + "\" " + (opts.items_per_page == arrPage[i] ? "selected" : "") + ">" + arrPage[i] + "</option>";
            }
            pageSelect += "</select>";
            panel.append("<li class='active'><a>共" + maxentries + "条，每页显示&nbsp;&nbsp;" + pageSelect + "</a></li>");
        }

        // Extract current_page from options
        var current_page = opts.current_page;
        // Create a sane value for maxentries and items_per_page
        //maxentries = (!maxentries || maxentries < 0) ? 1 : maxentries;
        opts.items_per_page = (!opts.items_per_page || opts.items_per_page < 0) ? 1 : opts.items_per_page;
        // Store DOM element for easy access from all inner functions
        var panel = jQuery(this).find("ul");
        // Attach control functions to the DOM element 
        this.selectPage = function (page_id) { pageSelected(page_id); }
        this.prevPage = function () {
            if (current_page > 0) {
                pageSelected(current_page - 1);
                return true;
            }
            else {
                return false;
            }
        }
        this.nextPage = function () {
            if (current_page < numPages() - 1) {
                pageSelected(current_page + 1);
                return true;
            }
            else {
                return false;
            }
        }
        // When all initialisation is done, draw the links
        //if (numPages() > 1) {
        drawLinks();
        //}

        // call callback function
        // opts.callback(current_page, this);
    });
}


