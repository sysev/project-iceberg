//Date related constants
var DAYS_OF_WEEK = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

var SUNDAY = 0;
var MONDAY = 1;
var TUESDAY = 2;
var WEDNESDAY = 3;
var THURSDAY = 4;
var FRIDAY = 5;
var SATURDAY = 6;

var JANUARY = 0;
var FEBRUARY = 1;
var MARCH = 2;
var APRIL = 3;
var MAY = 4;
var JUNE = 5;
var JULY = 6;
var AUGUST = 7;
var SEPTEMBER = 8;
var OCTOBER = 9;
var NOVEMBER = 10;
var DECEMBER = 11;

var TODAY = new Date();  //resetTime() called below

// Conversion factors as variables to eliminate all the multiplication
var SECOND     = 1000;
var MINUTE     = 60000;          // 60 * 1000
var HOUR       = 3600000;        // 60 * 60 * 1000
var DAY        = 86400000;       // 24 * 60 * 60 * 1000
var WEEK       = 604800000;      // 7 * 24 * 60 * 60 * 1000

//return a 4-digit number for dates independent of year.
function y2k(number)    { return (number < 1000) ? number + 1900 : number; }

//return the difference in (unit) between 2 dates.
//unit: S, s, m, h, d (see descriptions below)
function dateDiff(date1, date2, unit){
	if(date1.getTime() > date2.getTime()){
		date3 = date2;
		date2 = date1;
		date1 = date3;
	}

	diffInMillis = date2.getTime() - date1.getTime();
	
	switch(unit){
		case 'S':	//milliseconds
			return diffInMillis;
		case 's':	//seconds
			return diffInMillis / SECOND;
		case 'm':	//minutes
			return diffInMillis / MINUTE;
		case 'h':	//hours
			return diffInMillis / HOUR;
		case 'd':	//days
			return diffInMillis / DAY;		
		default:
			throw('Unit "' + unit + '" is not supported.');
	}
}

/*
Letter 	Date or Time 		Component	Presentation	Examples
a 	Am/pm marker 		Text 		PM
D 	Day in year 		Number 		189
d 	Day in month 		Number 		10
E 	Day in week 		Text 		Tuesday; Tue
h 	Hour in am/pm (1-12) 	Number 		12
H 	Hour in day (0-23) 	Number 		0
k 	Hour in day (1-24) 	Number 		24
K 	Hour in am/pm (0-11) 	Number 		0
m 	Minute in hour 		Number 		30
M 	Month in year 		Month 		July; Jul; 07
s 	Second in minute 	Number 		55
S 	Millisecond 		Number 		978
w 	Week in year 		Number 		27
W 	Week in month 		Number 		2
y 	Year 			Year 		1996; 96
z 	Time zone 		Number 		240 (difference in minutes from GMT)
*/
var DateFormat = {
    constructor: function(pattern) {
    	this.pattern = pattern;
    },    
    
    format: function(date){
    	if (!date) return '&nbsp;';
	df = this;
    	return this.pattern.replace(/(yyyy|yy|MMMM|MMM|MM|M|w|W|D|dddd|ddd|dd|d|EE|E|hh|h|HH|H|KK|K|kk|k|mm|m|ss|s|SSS|SS|S|a|z)/g,
    	    function($1)
    	    {
    	        switch ($1)
    	        {
    	        case 'yyyy': return date.getFullYear();
    	        case 'yy':   return date.getFullYear().toString().substr(-2);
    	        case 'MMMM': return MONTHS[date.getMonth()];
    	        case 'MMM':  return MONTHS[date.getMonth()].substr(0, 3);
    	        case 'MM':   return df.leadingZeros(date.getMonth() + 1, 2);
    	        case 'M':    return date.getMonth() + 1;
    	        case 'w':    return date.getWeekInYear();
    	        case 'W':    return date.getWeekInMonth();
    	        case 'D':    return date.getDayInYear(); 	        
    	        case 'dddd': return DAYS_OF_WEEK[date.getDay()];
    	        case 'ddd':  return DAYS_OF_WEEK[date.getDay()].substr(0, 3);
    	        case 'dd':   return df.leadingZeros(date.getDate(), 2);
    	        case 'd':    return date.getDate();
    	        case 'EE':   return DAYS_OF_WEEK[date.getDay()];
    	        case 'E':    return DAYS_OF_WEEK[date.getDay()].substr(0, 3);
    	        case 'hh':   return df.leadingZeros(((h = date.getHours() % 12) ? h : 12), 2);
    	        case 'h':    return ((h = date.getHours() % 12) ? h : 12);
    	        case 'HH':   return df.leadingZeros(date.getHours(), 2);
    	        case 'H':    return date.getHours();
    	        case 'KK':   return df.leadingZeros(((h = date.getHours() % 12) ? (h-1) : 11), 2);
    	        case 'K':    return ((h = date.getHours() % 12) ? (h-1) : 11);
    	        case 'kk':   return df.leadingZeros(date.getHours() + 1, 2);
    	        case 'k':    return date.getHours() + 1;
    	        case 'mm':   return df.leadingZeros(date.getMinutes(), 2);
    	        case 'm':    return date.getMinutes();
    	        case 'ss':   return df.leadingZeros(date.getSeconds(), 2);
    	        case 's':    return date.getSeconds();
    	        case 'SSS':  return df.leadingZeros(date.getMilliseconds(), 3);
    	        case 'SS':   return df.leadingZeros(date.getMilliseconds(), 2);
    	        case 'S':    return date.getMilliseconds();
    	        case 'a':    return date.getHours() < 12 ? 'am' : 'pm';
    	        case 'z':    return date.getTimezoneOffset();
    	        }
    	    }
    	);
    },
    
    parse: function(str){
    	if (!str) return null;
	df = this;
	var yyyy = new Number(str.substr(6));
	var MM = new Number(str.substr(0, 2));
	var dd = new Number(str.substr(3, 2));
	var date = new Date().resetTime();
	date.setFullYear(yyyy);
	date.setMonth(MM - 1);
	date.setDate(dd);	
	return date;
    },
    
    leadingZeros: function(num, len){
    	var str = num.toString();
    	if(str.length >= len) return num;
    	var zeros = (len-str.length);
    	for(var i=0; i<zeros; i++){
    		str = '0' + str;
    	}
    	return str;    	
    }
};

var DateRange = {
    constructor: function(fromDate, toDate){
        this.fromDate = fromDate;
        this.toDate = toDate;
    },
	
    toString: function(){
        return '[DateRange] From: ' + this.fromDate + '\nTo: ' + this.toDate;
    }
};

//in a non destructive manner, adds (amount) of (unit) to the current date.
//amount: any number, positive or negative
//unit:  S, s, m, h, d, w, M, y (see descriptions below)
Date.prototype.add = function(unit, amount){
	var date = this.clone();
	var tzo = date.getTimezoneOffset();
	switch(unit){
		case 'S':  //milliseconds			
			date.setTime(date.getTime() + amount);
			break;
		case 's':  //seconds
			date.setTime(date.getTime() + (amount * SECOND));
			break;
		case 'm':  //minutes
			date.setTime(date.getTime() + (amount * MINUTE));
			break;
		case 'h':  //hours
			date.setTime(date.getTime() + (amount * HOUR));
			break;
		case 'd':  //days
			date.setTime(date.getTime() + (amount * DAY));
			break;
		case 'w':  //weeks
			date.setTime(date.getTime() + (amount * WEEK));
			break;
		case 'M':  //months
			if(amount>=0){
				for(var n=0; n<amount; n++){
					var thisDaysInMonth = date.getDaysInMonth();
					for(var day=1; day<=thisDaysInMonth; day++){
						date = date.add('d', 1);
					}
				}
			}else{
				for(var n=0; n<Math.abs(amount); n++){
					var lastMonth = date.clone();
					lastMonth.setMonth((date.getMonth() ==12) ? 0 : date.getMonth()-1);
					var lastMonthsDays = lastMonth.getDaysInMonth();
					for(var day=1; day<=lastMonthsDays; day++){
						date = date.add('d', -1);
					}
				}		
			}
			break;
			
		case 'y': //years
			date.setYear(date.getFullYear() + amount);
			break;
		default:
			throw('Invalid unit "' + unit + '" specified in Date.add(unit, amount).');			
	}
	date.setTime(date.getTime() + (date.getTimezoneOffset() - tzo) * MINUTE); //adjust for daylight savings
	return date;
}

//returns true if the current year is a leap year, false if not.
Date.prototype.isLeapYear = function(){
  var year = this.getFullYear();
  if (0 == year % 400) return true;
  if (0 == year % 100) return false;
  return (0 == year % 4) ? true : false;
}

//return true if it's a weekday, false if it's on the weekend
Date.prototype.isWeekDay = function(){
  var dayOfWeek = this.getDay();
  return !(dayOfWeek == SATURDAY || dayOfWeek == SUNDAY);
}

//return the number of days in the current month
Date.prototype.getDaysInMonth = function(){
	switch(this.getMonth()){
		case JANUARY:
			return 31;
		case FEBRUARY:
			return (this.isLeapYear()) ? 29 : 28;
		case MARCH:
			return 31;
		case APRIL:
			return 30;
		case MAY:
			return 31;
		case JUNE:
			return 30;
		case JULY:
			return 31;
		case AUGUST:
			return 31;
		case SEPTEMBER:
			return 30;
		case OCTOBER:
			return 31;
		case NOVEMBER:
			return 30;
		case DECEMBER:
			return 31;
		default:
			throw(this.getMonth() + " is an invalid date month.");	
	}
}

//returns the nth day of the current year.  example: Feb 2 = day 33.
Date.prototype.getDayInYear = function(){
	var dayInYear = this.getDate();
    switch(this.getMonth()){
		case DECEMBER: 	dayInYear += 30;
        case NOVEMBER: 	dayInYear += 31;
		case OCTOBER: 	dayInYear += 30;
        case SEPTEMBER:	dayInYear += 31;
		case AUGUST:	dayInYear += 31;
		case JULY:		dayInYear += 30;
		case JUNE:		dayInYear += 31;
		case MAY:		dayInYear += 30;
		case APRIL:		dayInYear += 31;
		case MARCH:		(this.isLeapYear()) ? dayInYear += 29 : dayInYear += 28;
		case FEBRUARY:	dayInYear += 31;
		default:	return dayInYear;
	}
}

//using Sunday as the first day of the week, and counting partial weeks, 
//will return the current week of the current year (1-53)
Date.prototype.getWeekInYear = function(){
	return Math.ceil((this.getDayInYear() + this.getYearStartsOn())/7);
}

//using Sunday as the first day of the week, and counting partial weeks, 
//will return the current week of the current month (1-6)
Date.prototype.getWeekInMonth = function(){
	return Math.ceil((this.getDate() + this.getMonthStartsOn())/7);
}

// returns the day of the week (0-6) of the first day of this month
Date.prototype.getMonthStartsOn = function(){
	var date = this.clone();
	date.setDate(1);
	return date.getDay();
}

// returns the day of the week (0-6) of the last day of this month
Date.prototype.getMonthEndsOn = function(){
	var date = this.clone();
	date.setDate(date.getDaysInMonth());
	return date.getDay();
}

// returns the day of the week (0-6) of the first day of the current year
Date.prototype.getYearStartsOn = function(){
	var date = this.clone();
	date.setMonth(JANUARY);
	date.setDate(1);
	return date.getDay();
}

// sets the time to midnight
Date.prototype.resetTime = function(){
	this.setHours(0);
	this.setMinutes(0);
	this.setSeconds(0);
	this.setMilliseconds(0);
	return this;
}
TODAY = TODAY.resetTime();

// create a copy of a date in its own instance
Date.prototype.clone = function(){
	return new Date(new Date().setTime(this.getTime()));
}

//returns a 4-digit number for dates independent of year.
Date.prototype.getY2KYear = function(){
	var year = this.getYear();
	return (year < 1000) ? year + 1900 : year;
}