chart = new Highcharts.Chart({
    credits: { "enabled": false }, //不显示右下角网址链接
    chart: {
        renderTo: 'Linecontainer', 
        defaultSeriesType: 'line',
        marginRight: 20
    },
    title: {
        text: '{#title#}'
    },
    xAxis: {
        categories: [#categries#],
        labels: {
            rotation: -45, //字体倾斜
            align: 'right',
            style: { font: 'normal 13px 宋体' }
        }
    },
    yAxis: {
        min:0,
        title: {
            text: '{#yAxis#}'
        },
        labels: {
            formatter: function() {
                //return this.value/1000+'#Yunit#';
                return this.value+'#Yunit#';
            }
        }
    },
    tooltip: {
        formatter: function() {
            return this.series.name+'#tooltip#:<b><br/>'+
				Highcharts.numberFormat(this.y, 0) +'</b>#unit#';
        }
    },
    plotOptions: {
        area: {
            pointStart: 0,
            marker: {
                enabled: false,
                symbol: 'circle',
                radius: 2,
                states: {
                    hover: {
                        enabled: true
                    }
                }
            }
        }
    },
    series: [{#series#}]
});