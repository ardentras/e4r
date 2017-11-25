import React from 'react';

export default function DescriptinCells(props) {
    return (
        <div className="four-cells">
            <div className="cells">
                <h1>MISSION</h1>
                <p>
                    Our Mission is to provide a
                    platform for people to
                    donate without anything out
                    of pocket.  All donations
                    will be made through ad
                    revenue.
                </p>
                <span className="learn-more-but">LEARN MORE</span>
            </div>
            <div className="cells">
                <h1>SOLVE</h1>
                <p>
                    We offer a wide range of 
                    subjects for users to solve
                    such as History, Science,
                    and Math.  Users can solve
                    these questionairs to donate
                    money.
                </p>
                <span className="learn-more-but">LEARN MORE</span>
            </div>
            <div className="cells">
                <h1>LEARN</h1>
                <p>
                    Through our application, we
                    provide how-to steps to slove
                    many of the questionairs we
                    provide so users will be able
                    to learn and grow 
                    academically.
                </p>
                <span className="learn-more-but">LEARN MORE</span>
            </div>
            <div className="cells">
                <h1>DONATE</h1>
                <p>
                    We work by providing
                    our users many selections
                    of reputable choices of
                    charities such as Red Cross,
                    Life for Asians and BeGood.
                </p>
                <span className="learn-more-but">LEARN MORE</span>
            </div>
        </div>
    );
}