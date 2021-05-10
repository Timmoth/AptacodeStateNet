using System.Collections.Generic;
using Aptacode.Expressions;
using Aptacode.Expressions.List.IntegerListOperators;
using Aptacode.StateNet.Engine.Transitions;
using Aptacode.StateNet.Network;
using Aptacode.StateNet.PatternMatching;
using Aptacode.StateNet.PatternMatching.Expressions;

namespace StateNet.Tests.Network.Helpers
{
    public static class StateNetworkDictionary_Helpers
    {
        private static readonly ExpressionFactory<TransitionHistory> Expressions =
            new();

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Minimal_Valid_Connected_StaticWeight_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("b", Expressions.Int(1))
                            }
                        }
                    }
                },
                {
                    "b", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("a", Expressions.Int(1))
                            }
                        }
                    }
                }
            };

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Invalid_UnusableInput_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("b", Expressions.Int(1))
                            }
                        }
                    }
                },
                {
                    "b", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("a", Expressions.Int(1))
                            }
                        },
                        {
                            "2", new List<Connection>()
                        }
                    }
                }
            };

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Invalid_Unreachable_State_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("b", Expressions.Int(1))
                            }
                        }
                    }
                },
                {
                    "b", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("a", Expressions.Int(1))
                            }
                        }
                    }
                },
                {
                    "c", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>()
                        }
                    }
                }
            };

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Invalid_ConnectionTargetState_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("c", Expressions.Int(1))
                            }
                        }
                    }
                },
                {
                    "b", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("a", Expressions.Int(1))
                            }
                        }
                    }
                }
            };

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Invalid_ConnectionPatternState_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("b", new Count<int, TransitionHistory>(new Matches(new Pattern("c"))))
                            }
                        }
                    }
                },
                {
                    "b", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("a", Expressions.Int(1))
                            }
                        }
                    }
                }
            };

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Invalid_ConnectionPatternInput_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("b", new Count<int, TransitionHistory>(new Matches(new Pattern("bi"))))
                            }
                        }
                    }
                },
                {
                    "b", new Dictionary<string, IEnumerable<Connection>>
                    {
                        {
                            "1", new List<Connection>
                            {
                                new("a", Expressions.Int(1))
                            }
                        }
                    }
                }
            };

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            Empty_NetworkDictionary =>
            new();

        public static Dictionary<string, Dictionary<string, IEnumerable<Connection>>>
            SingleState_NetworkDictionary =>
            new()
            {
                {
                    "a", new Dictionary<string, IEnumerable<Connection>>()
                }
            };
    }
}