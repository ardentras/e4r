let canvas = null;
let ctx = null;
let Circles = [];

window.onload = function() {
    canvas = document.getElementById("grid");

    window.addEventListener("resize", ()=>{
        canvas.width = window.innerWidth * 0.86;
        canvas.height = window.innerHeight;
    });
    ctx = canvas.getContext("2d");
    fillCircle(100);
    animate();
}

class Circle {
    constructor(x,y,dx,dy,radius,color) {
        this.x = x;
        this.y = y;
        this.tempx = x;
        this.dx = dx;
        this.dy = dy;
        this.radius = radius;
        this.color = color;
        this.draw = this.draw.bind(this);
    }
    draw() {
        ctx.fillStyle = this.color;
        ctx.beginPath();
        ctx.lineWidth = 0;
        ctx.arc(this.x,this.y,this.radius,Math.PI*2,false);
        ctx.closePath();
        ctx.fill();
        ctx.strokeStyle = this.color;
        ctx.stroke();
    }
    update() {
        if (this.x >= this.tempx + 10 + this.radius || this.x <= this.tempx - 10 - this.radius) {
            this.dx = -this.dx;
        }
        if (this.y + this.radius < 0) {
            this.y = canvas.height + this.radius + 5;
            this.x = Math.random() * (canvas.width - 10);
            this.tempx = this.x;
        }

        this.x += this.dx;
        this.y += this.dy;

        this.draw();
    }
};

function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0,0,canvas.width, canvas.height);

    background();
    for(let i = 0; i < Circles.length; ++i) {
        Circles[i].update();
    }
    monthlyTitle();
    roundedButton();
    
};

function roundedButton() {
    //Draw a rectangle shape;
    ctx.fillStyle ="white";
    ctx.fillRect(canvas.width/2.1,220, 110, 33);

    //draw an arc or circle at the right end of the rectangle
    ctx.beginPath();
    ctx.arc(canvas.width/2.1 + 4.2,236.7,16.1,Math.PI * 2,false);
    ctx.fillStyle = "white";
    ctx.fill();
    ctx.lineWidth = 0;
    ctx.strokeStyle = "white";
    ctx.stroke();

    //draw a smaller rectangle inside the larger rectangle
    ctx.fillStyle ="#333F50";
    ctx.fillRect(canvas.width/2.1 + 1.5,221.5, 107, 30);

    //draw a smaller arc or circle inside the largest arc
    ctx.beginPath();
    ctx.arc(canvas.width/2.1 + 4,236.4,14.2,Math.PI * 2,false);
    ctx.fillStyle = "#333F50";
    ctx.fill();
    ctx.lineWidth = 0;
    ctx.strokeStyle = "#333F50";
    ctx.stroke();

    //draw a month and year text inside button
    monthStatistic();

    //draw expand arrow

    downArrow();
};

function background() {
    ctx.fillStyle = "#333F50";
    ctx.fillRect(0,0,canvas.width, canvas.height);
};

function monthlyTitle() {
    ctx.font = "3em Arial";
    ctx.fillStyle = "#FF837E";
    ctx.fillText("Total Monthly Donations", canvas.width/3, 150);
};

function monthStatistic() {
    ctx.font = "15px Arial";
    ctx.fillStyle = "white";
    ctx.fillText("Jan, 2017", canvas.width/2, 242);
};

function downArrow() {
    ctx.beginPath();
    ctx.moveTo(canvas.width/2.1, 232);
    ctx.lineTo(canvas.width/2.1+7,241);
    ctx.closePath();
    ctx.strokeStyle = "white";
    ctx.stroke();

    ctx.beginPath();
    ctx.moveTo(canvas.width/2.1 + 14, 232);
    ctx.lineTo(canvas.width/2.1 + 6,241);
    ctx.closePath();
    ctx.strokeStyle = "white";
    ctx.stroke();
};

function fillCircle(size) {
    const colors = ["#313542", 
    "#ED6541", 
    "#E2EDF3"];

    for(let i = 0;  i < size; ++i) {
        Circles.push(new Circle(Math.random() * (canvas.width - 10),canvas.height + ((Math.random()+2) * 200),0.3, (Math.random() + 1) * - 2, Math.random() * 20, colors[Math.floor(Math.random() * 5)]));
    }
}