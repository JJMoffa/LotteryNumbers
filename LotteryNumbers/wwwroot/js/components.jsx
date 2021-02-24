class Line extends React.Component {
    render() {
        var balls = this.props.children;
        return (
            <table>
                <tbody>
                    <tr> 
                        {balls.map(function (ball, index) {
                            var ballStyle = {
                                borderRadius: "50%",
                                width: "34px",
                                height: "34px",
                                padding: "10px",
                                background: ball.colourId,
                                border: "3px solid #000",
                                color: "#000",
                                textAlign: "center",
                                font: "32px Arial"
                            };
                            return <td key={index}><div style={ballStyle}>{ball.number}</div></td>
                        })}             
                    </tr>
                </tbody>
            </table>
        );
    }
}

class LineList extends React.Component {
    render() {
        const lineNodes = this.props.data.map(line => (
            <Line created={line.created} key={line.id}>
                { line.balls }
            </Line>
        ));
        return <div className="lineList">{lineNodes}</div>;
    }
}

class NewLinesForm extends React.Component {
    state = {
        numberoflines: "1",
        bonusball: false
    };

    handleNumberOfLinesChange = e => {
        this.setState({ numberoflines: e.target.value });
    };

    handleBonusBallToggle = e => {
        this.setState({ bonusball: e.target.checked });
    };

    handleSubmit = e => {
        e.preventDefault();
        var numberoflines = this.state.numberoflines;
        var bonusball = this.state.bonusball;
       
        this.props.onNewLinesSubmit({ numberoflines: numberoflines, bonusball: bonusball });
    };

    render() {
        return (
            <form className="newlinesform" onSubmit={this.handleSubmit}>
                <label htmlFor="lines">Number Of Lines</label>
                <input
                    type="number"
                    min="1"
                    max="10" 
                    value={this.state.numberoflines}
                    onChange={this.handleNumberOfLinesChange}
                />

                <label htmlFor="bonusball">Bonus Ball</label>
                <input
                    type="checkbox"
                    value={this.state.bonusball}
                    onChange={this.handleBonusBallToggle}
                />
       
                <input type="submit" value="Generate Numbers" />
            </form>
        );
    }
}

class LineBox extends React.Component {


    constructor(props) {
        super(props);
        this.state = { data: [] };
        this.handNewLinesSubmit = this.handNewLinesSubmit.bind(this);
    }
    loadLinesFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    handNewLinesSubmit(newlines) {
        const data = new FormData();
        data.append('NumberOfLines', newlines.numberoflines);
        data.append('BonusBall', newlines.bonusball);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = () => this.loadLinesFromServer();
        xhr.send(data);
    }
    componentDidMount() {
        this.loadLinesFromServer();
    }
    render() {
        return (
            <div className="lineBox">
                <LineList data={this.state.data} />
                <NewLinesForm onNewLinesSubmit={this.handNewLinesSubmit} />
            </div>
        );
    }
}

ReactDOM.render(
    <LineBox
        url="/lotterylines"
        submitUrl="/lotterylines/new"
    />,
    document.getElementById('content'),
);