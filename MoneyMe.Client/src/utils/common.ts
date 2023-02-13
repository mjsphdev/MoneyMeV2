export function dateFormat(dateToConvert: Date){
       
    const date = new Date(dateToConvert);
    const options = { month: '2-digit', day: '2-digit', year: 'numeric' };
    const formattedDate = date.toLocaleDateString('en-US', options);

    return formattedDate;
} 