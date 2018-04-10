import React from "react";
import Styles from "./style.css";

class Leaderboard extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.leaderboard}>
                <ul className={Styles.heading}>
                    <li className={Styles.rankhead}>Rank</li>
                    <li className={Styles.namehead}>Name</li>
                    <li className={Styles.solvehead}>Solved</li>
                </ul>
                <ul className={Styles.ranking}>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>0</span><span className={Styles.name}>User1253</span><span className={Styles.totalsolved}>163</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>1</span><span className={Styles.name}>User1492</span><span className={Styles.totalsolved}>144</span></div> </li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>2</span><span className={Styles.name}>User2300</span><span className={Styles.totalsolved}>102</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>3</span><span className={Styles.name}>User0003</span><span className={Styles.totalsolved}>60</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>4</span><span className={Styles.name}>User0213</span><span className={Styles.totalsolved}>44</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>5</span><span className={Styles.name}>User0003</span><span className={Styles.totalsolved}>32</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>6</span><span className={Styles.name}>User1123</span><span className={Styles.totalsolved}>31</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>7</span><span className={Styles.name}>User1563</span><span className={Styles.totalsolved}>30</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>8</span><span className={Styles.name}>User2583</span><span className={Styles.totalsolved}>27</span></div></li>
                    <li className={Styles.rank}><div className={Styles.person}><span className={Styles.number}>9</span><span className={Styles.name}>User1038</span><span className={Styles.totalsolved}>26</span></div></li>
                </ul>
            </div>
        );
    }
}

export default Leaderboard;