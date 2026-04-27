const n=(t,r=2)=>new Intl.NumberFormat("es-MX",{style:"currency",currency:"MXN",minimumFractionDigits:r,maximumFractionDigits:r}).format(t);export{n as f};
