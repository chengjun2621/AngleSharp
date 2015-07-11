﻿namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the CSSGroupingRule interface.
    /// </summary>
    abstract class CssGroupingRule : CssRule, ICssGroupingRule
    {
        #region Fields

        readonly CssRuleList _rules;
        readonly CssParserOptions _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grouping rule.
        /// </summary>
        internal CssGroupingRule(CssRuleType type, CssParserOptions options)
            : base(type)
        {
            _rules = new CssRuleList();
            _options = options;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all CSS rules contained within the grouping block.
        /// </summary>
        public CssRuleList Rules
        {
            get { return _rules; }
        }

        ICssRuleList ICssGroupingRule.Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Internal Properties

        internal CssParserOptions Options 
        {
            get { return _options; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Used to insert a new rule into the media block.
        /// </summary>
        /// <param name="rule">
        /// The parsable text representing the rule. For rule sets this contains
        /// both the selector and the style declaration. For at-rules, this
        /// specifies both the at-identifier and the rule content.
        /// </param>
        /// <param name="index">
        /// The index within the media block's rule collection of the rule before
        /// which to insert the specified rule.
        /// </param>
        /// <returns>
        /// The index within the media block's rule collection of the newly
        /// inserted rule.
        /// </returns>
        public Int32 Insert(String rule, Int32 index)
        {
            var value = CssParser.ParseRule(rule, _options);
            _rules.Insert(value, index, Owner, this);
            return index;    
        }

        /// <summary>
        /// Used to delete a rule from the media block.
        /// </summary>
        /// <param name="index">
        /// The index within the media block's rule collection of the rule to remove.
        /// </param>
        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        #endregion

        #region Internal Methods

        internal void AddRule(CssRule rule)
        {
            if (rule != null)
                _rules.Add(rule, Owner, this);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssGroupingRule)rule;
            _rules.Clear();
            _rules.Import(newRule._rules, Owner, Parent);
        }

        #endregion
    }
}
