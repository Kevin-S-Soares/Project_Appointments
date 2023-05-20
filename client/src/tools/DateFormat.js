export class DateFormat{
    static getDate(date){
        return (date.getDate() < 10? '0' + date.getDate(): date.getDate()) + "/" 
        + (date.getMonth() < 10? '0' + date.getMonth(): date.getMonth()) + "/" 
        + date.getFullYear();
    }

    static getTime(date){
        return (date.getHours() < 10? '0' + date.getHours(): date.getHours()) + ":" 
        + (date.getMinutes() < 10? '0' + date.getMinutes(): date.getMinutes()) + ":" 
        + (date.getSeconds() < 10? '0' + date.getSeconds(): date.getSeconds());
    }

    static getDateTime(date){
        return this.getDate(date) + " " + this.getTime(date);
    }
}