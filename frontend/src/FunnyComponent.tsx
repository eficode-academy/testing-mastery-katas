import React from "react";

export class FunnyComponent extends React.Component<
  {},
  {
    error: { message: string } | null;
    isLoaded: boolean;
    joke: { text: string } | null;
  }
> {
  constructor(props: {}) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      joke: null,
    };
  }

  componentDidMount() {
    fetch("https://localhost:5000/Joke", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify("a"),
    })
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            joke: result,
          });
        },
        // Note: it's important to handle errors here
        // instead of a catch() block so that we don't swallow
        // exceptions from actual bugs in components.
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );
  }

  render() {
    const { error, isLoaded, joke } = this.state;
    if (error) {
      return <div>Error: {error.message}</div>;
    } else if (!isLoaded) {
      return <div>Loading...</div>;
    } else {
      return <div>{joke?.text}</div>;
    }
  }
}
