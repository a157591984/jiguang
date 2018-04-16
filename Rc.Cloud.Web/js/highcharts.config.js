Highcharts.theme = {
    //colors: ['#1BB674', '#28A9E3', '#FF7D63', '#FFCA51', '#EF4349', '#2B908F', '#7798BF'],
    title: {
        text: '',
        style: {
            fontFamily: '"Microsoft YaHei",arial'
        }
    },
    subtitle: {
        text: '',
        style: {
            fontFamily: '"Microsoft YaHei",arial'
        }
    },
    credits: {
        enabled: false
    },
    legend: {
        verticalAlign: 'top',
        itemStyle: {
            fontWeight: "normal",
            fontFamily: '"Microsoft YaHei",arial'
        },
    },
    tooltip: {
        style: {
            fontFamily: '"Microsoft YaHei",arial',
            fontWeight: "normal",
            textShadow: "none"
        },
        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
        '<td style="padding:0"><b>{point.y}</b></td></tr>',
        footerFormat: '</table>',
        shared: true,
        useHTML: true
    },
    plotOptions: {
        column: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            },
            pointWidth: 30
        },
        line: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            }
        },
        spline: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            }
        },
        bar: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            },
            pointWidth: 30
        }
    },
    s: {
        style: {
            fontFamily: '"Microsoft YaHei",arial'
        }
    },
    xAxis: {
        title: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        },
        labels: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        }
    },
    yAxis: {
        title: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        },
        labels: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        },
        allowDecimals: false
    }
}
var highchartsOptions = Highcharts.setOptions(Highcharts.theme);